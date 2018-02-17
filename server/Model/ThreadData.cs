using ExchangeSharp;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aymk_engine.Model
{
     
    public class ThreadData
    {        
        public CatalogMarket market { get; set; }       
        public string name { get; set; } 
    }
}
