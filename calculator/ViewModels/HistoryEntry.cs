namespace calculator.ViewModels
{
    public class HistoryEntry
    {
        #region Declarations
        
        private string _id;
        private string _action;
        private string _value;

        #endregion Declarations

        #region Properties

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion Properties

        #region Constructor

        public HistoryEntry(
            string action,
            string id,
            string value)
        {
            Action = action;
            ID = id;
            Value = value;
        }

        #endregion Constructor
    }
}
