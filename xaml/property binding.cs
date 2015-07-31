public class SomeData: INotifyPropertyChanged
    {

 public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        bool _someField; 
        public bool SomeProperty
        {
            get 
            {
                return _someField; 
            }
            set
            {
                _someField = value;
                NotifyPropertyChanged("SomeProperty");
            }
        }
}