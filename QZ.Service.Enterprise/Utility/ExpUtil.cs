/*
 * This class providers some method tools to adapt the QZ.Instrument.Experiments
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using QZ.Instrument.Experiments;

namespace QZ.Service.Enterprise
{
    public class ExpUtil
    {
        //[Conditional("DEBUG")]
        //public static void Observation_Insert(string script, string name, string query, double[] scores, double[] weights, double[] weightexts = null)
        //{
        //    var doc = new Observation();
        //    doc.Name = name;
        //    doc.ObId = $"{name}|{query}|{script}";//, name, query, script, weights, weightexts, scores";
        //    doc.Query = query;
        //    doc.Score = scores;
        //    doc.Script = script;
        //    doc.Weight = weights;
        //    doc.WeightExt = weightexts;
        //    ScoreIO.InsertAsync(doc);
        //}
    }
}