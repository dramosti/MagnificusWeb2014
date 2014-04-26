using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLP.Wcf.Entries
{
    internal class ModelDefinition
    {
        public static object[] ARRAY_OBJECTS = new object[] { };

        public string TypeName { get; set; }

        private List<ModelProperty> _properties =
            new List<ModelProperty>();

        internal List<ModelProperty> Properties
        {
            get { return _properties; }
            set { _properties = value; }
        }

        internal static List<ModelDefinition> _ModelDefinitions =
            new List<ModelDefinition>();

        internal static ModelDefinition GetModelDefinition(Type modelClass)
        {
            string typeName = modelClass.Name;
            ModelDefinition definition =
                (from d in ModelDefinition._ModelDefinitions
                 where d.TypeName == typeName
                 select d).FirstOrDefault();
            if (definition == null)
            {
                definition = new ModelDefinition();
                definition.TypeName = typeName;
                definition.Properties = (from p in modelClass.GetProperties()
                                         select new
                                         {
                                             Name = p.Name,
                                             OrderDefinition = (HLP.Comum.Infrastructure.ParameterOrder)(p.GetCustomAttributes(typeof(HLP.Comum.Infrastructure.ParameterOrder), true).FirstOrDefault())
                                         })
                                .Where(p => p.OrderDefinition != null)
                                .OrderBy(p => p.OrderDefinition.Order)
                                .Select(p => new ModelProperty() { Name = p.Name, Order = p.OrderDefinition.Order })
                                .ToList();
                _ModelDefinitions.Add(definition);
            }

            return definition;
        }
    }

    internal class ModelProperty
    {
        public string Name { get; set; }
        public int Order { get; set; }
    }
}