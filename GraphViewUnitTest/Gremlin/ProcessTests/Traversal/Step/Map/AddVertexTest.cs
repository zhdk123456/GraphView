﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphView;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace GraphViewUnitTest.Gremlin
{
    [TestClass]
    public class AddVertexTest : AbstractGremlinTest
    {
        [TestMethod]
        [TestModernCompatible]
        public void Test_g_V_AddV()
        {
            using (GraphViewCommand GraphViewCommand = new GraphViewCommand(graphConnection))
            {
                GraphViewCommand.OutputFormat = OutputFormat.GraphSON;
                GraphViewCommand.g().AddV("V1").Property(TEST_PARTITION_BY_KEY, "Value1").Next();
                GraphViewCommand.g().AddV("V2").Property(TEST_PARTITION_BY_KEY, "Value2").Next();
                GraphViewCommand.g().AddV("V3").Property(TEST_PARTITION_BY_KEY, "Value3").Next();
                var traversal = GraphViewCommand.g().V().AddV().Property(TEST_PARTITION_BY_KEY, "PV");
                dynamic result = JsonConvert.DeserializeObject<dynamic>(traversal.Next().FirstOrDefault());
                Console.WriteLine(result);
            }
        }
    }
}
