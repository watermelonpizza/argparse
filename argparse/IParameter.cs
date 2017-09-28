using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace argparse
{
    public interface IParameter : IComparable<IParameter>
    {
        /// <summary>
        /// The name of the paramter which will be displayed in the help documentation
        /// </summary>
        string ParameterName { get; }

        /// <summary>
        /// The unique position id for the parameter
        /// </summary>
        uint Position { get; }

        /// <summary>
        /// The type of the parameter (i.e. the type of the property)
        /// </summary>
        Type ParameterType { get; }

        /// <summary>
        /// If the parameter is required
        /// </summary>
        bool IsRequired { get; }

        /// <summary>
        /// If the parameter can accept multiple values (must be the last positioned parameter)
        /// </summary>
        bool IsMultiple { get; }

        /// <summary>
        /// The help documentation for the parameter
        /// </summary>
        string ParameterHelp { get; }

        /// <summary>
        /// The details for the property that is bound to the parameter
        /// </summary>
        PropertyInfo Property { get; }
    }

    public interface IParameter<TCatagory, TParameter> : IParameter, IWithParameter<TCatagory>, ICreateParameterCatagory
    {
        /// <summary>
        /// Sets the name of the parameter which is used for help documentation
        /// </summary>
        IParameter<TCatagory, TParameter> Name(string name);

        /// <summary>
        /// Sets the parameter to be required
        /// </summary>
        IParameter<TCatagory, TParameter> Required();

        /// <summary>
        /// Sets the help documentation for the parameter
        /// </summary>
        IParameter<TCatagory, TParameter> Help(string help);
    }
}
