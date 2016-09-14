using System;

namespace Helpers
{
    public class HandledProperty<T>
    {
        public HandledProperty()
        {
        
        }

        public HandledProperty(T defaultVlalue)
        {
            _value = defaultVlalue;
        }

        public event Action<T> OnPropertyUpdated;
        private T _value;

        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                if (OnPropertyUpdated != null)
                    OnPropertyUpdated(value);
            }
        }
    }
}