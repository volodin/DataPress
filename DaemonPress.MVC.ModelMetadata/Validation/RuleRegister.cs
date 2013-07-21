using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using StructureMap.Configuration.DSL;

    internal class RuleRegistry : Registry
    {
        public RuleRegistry()
        {
            ForSingletonOf<IModelValidatorRule>().
                Use<RequireRule>().Named(VALIDATOR_TYPE.Required.ToString());

            ForSingletonOf<IModelValidatorRule>().
                Use<RangeRule>().Named(VALIDATOR_TYPE.Range.ToString());

            ForSingletonOf<IModelValidatorRule>().
                Use<StringLengthRule>().Named(VALIDATOR_TYPE.StringLength.ToString());

            ForSingletonOf<IModelValidatorRule>().
                Use<RegularExpressionRule>().Named(RegularExpressionRule.Name);

            ForSingletonOf<IModelValidatorRule>().
               Use<RemoteRule>().Named(VALIDATOR_TYPE.Remote.ToString());

            ForSingletonOf<IModelValidatorRule>().
                Use<EqualRule>().Named(VALIDATOR_TYPE.Equal.ToString());
        }
    }
}
