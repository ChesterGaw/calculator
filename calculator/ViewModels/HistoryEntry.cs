namespace calculator.ViewModels
{
    /// <summary>
    /// Object that corresponds to each history entry
    /// </summary>
    public class HistoryEntry
    {
        #region Declarations

        private string _action;
        private string _id;        
        private string _value;

        #endregion Declarations

        #region Properties

        /// <summary>
        /// History action
        /// </summary>
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        /// <summary>
        /// History ID
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        /// <summary>
        /// History value
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// History entry constructor
        /// </summary>
        /// <param name="action"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public HistoryEntry(
            string action,
            string id,
            string value)
        {
            Action = action;
            Id = id;
            Value = value;
        }

        #endregion Constructor
    }
}
