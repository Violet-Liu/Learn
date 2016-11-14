#light
module Cipher
open System
open System.Security.Cryptography
open System.Text
open System.IO
open System.IO.Compression

let ToMd5_16 (input:string) =
    let str =Encoding.Default.GetBytes input 
                |> (new MD5CryptoServiceProvider()).ComputeHash
                |> BitConverter.ToString
    str.Replace('-', '\u0000').ToLower().Substring(8, 16);

/// Just for compatibility where a mistake was made in history
let ToMd5_16_Fake (input:string) =
    let str =Encoding.Default.GetBytes input 
                |> (new MD5CryptoServiceProvider()).ComputeHash
                |> BitConverter.ToString
    str.Replace('-', '\u0000').ToLower().Substring(9, 16);

let AES_Create key =
    let aes = new RijndaelManaged()
    do
        aes.BlockSize <- 128
        aes.KeySize <- 256
        aes.Mode <- CipherMode.CBC
        aes.Padding <- PaddingMode.PKCS7
        aes.Key <- key
        aes.IV <- Array.create 16 0uy   // Array.zeroCreate<byte> 16
    aes


let EncryptFromBytes input key =
    let aes = AES_Create key
    let encrypt = aes.CreateEncryptor(aes.Key, aes.IV)
    using (new MemoryStream())
          (fun ms -> using (new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                           (fun cs -> cs.Write(input, 0, input.Length)
                                      ms.ToArray()))

let Encrypt2Bytes (input:string) (key:string) = 
    EncryptFromBytes (Encoding.UTF8.GetBytes input) (Encoding.UTF8.GetBytes key)

let Encrypt2Base64 input key = Encrypt2Bytes input key |> Convert.ToBase64String

let Decrypt2Bytes input key =
    let aes = AES_Create key
    let decrypt = aes.CreateDecryptor()
    using (new MemoryStream())
          (fun ms -> using(new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                          (fun cs -> cs.Write(input, 0, input.Length)
                                     ms.ToArray()))

let DecryptFromBase64 input (key:string) =
    Decrypt2Bytes (Convert.FromBase64String input) (Encoding.UTF8.GetBytes key) |> Encoding.UTF8.GetString

/// Decompress a byte array with Gzip
let DecompressFromBytes (input: byte[]) =
    using(new MemoryStream(input))
         (fun ms -> using(new GZipStream(ms, CompressionMode.Decompress))
                         (fun gzip -> using(new MemoryStream())
                                          (fun msi -> gzip.CopyTo(msi, 0x2000)
                                                      msi.ToArray())))

/// Decompress a base64 string with Gzip
let DecompressFromString input =
    Convert.FromBase64String input |> DecompressFromBytes |> Encoding.UTF8.GetString

let CompressFromBytes (input: byte[]) =
    using(new MemoryStream(input.Length))
         (fun ms -> using(new GZipStream(ms, CompressionMode.Compress))
                         (fun gzip -> gzip.Write(input, 0, input.Length)
                                      gzip.Flush()
                                      ms.ToArray()))

let CompressFromString (input: string) =
    Encoding.UTF8.GetBytes input |> CompressFromBytes |> Convert.ToBase64String

let Safe_Decrypt input key =
    try
        DecryptFromBase64 input key |> Some
    with
        | _ -> None     // swallow all exception