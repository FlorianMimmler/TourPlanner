using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Stores
{
    public class SelectedTabStore
    {
        public event Action SelectedTabChanged;

        private TabType _selectedTab;
        public TabType SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                _selectedTab = value;
                SelectedTabChanged?.Invoke();
            }
        }
    }
}
