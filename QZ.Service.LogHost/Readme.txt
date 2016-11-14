Install windows service steps:
1. Open MSMQ service
2. Build project at 'Release' mode
3. Copy all built files into a path.
4. Open vs cmd prompt tool at Administrator mode
5. Type 'installutil [Path]/[install exe file name]'   /* eg. QZ.Service.LogHost.exe */
6. Type 'net start logservice'

note:
1. must not create the msmq manually, otherwise this logservice cannot start successfully.