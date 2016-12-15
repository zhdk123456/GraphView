﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace GraphView.GremlinTranslationOps.map
{
    internal class GremlinPropertiesOp: GremlinTranslationOperator
    {
        public List<object> PropertyKeys;

        public GremlinPropertiesOp(params object[] propertyKeys)
        {
            PropertyKeys = new List<object>();
            foreach (var propertyKey in  propertyKeys)
            {
                PropertyKeys.Add(propertyKey);
            }
        }

        public override GremlinToSqlContext GetContext()
        {
            GremlinToSqlContext inputContext = GetInputContext();

            var secondTableRef = GremlinUtil.GetSchemaObjectFunctionTableReference("properties", PropertyKeys);

            var newVariable = inputContext.CrossApplyToVariable(inputContext.CurrVariable, secondTableRef, Labels);
            inputContext.SetCurrVariable(newVariable);
            inputContext.SetDefaultProjection(newVariable);

            return inputContext;
        }
    }
}
