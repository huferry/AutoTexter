namespace AutoTexter
{
    /// <summary>
    ///     AutoText variables and values.
    /// </summary>
    public class AutoTextValues
    {
        /// <summary>
        ///     Construct AutoTextValues
        /// </summary>
        /// <param name="variables">The vaiables as pipe separated string.</param>
        /// <param name="values">The list of values as pipe separated string.</param>
        public AutoTextValues(string variables, params string[] values)
        {
            Variables = variables;
            Values = values;
        }

        /// <summary>
        ///     List of variables as pipe separated string.
        /// </summary>
        public string Variables { get; }

        /// <summary>
        ///     List of list of values as pipe separated string.
        /// </summary>
        public string[] Values { get; }
    }
}