using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGallery.Common.Models
{
    public class Category
    {
        public Category(string name, Symbol icon, string tag, string description, ObservableCollection<Category> children = null)
        {
            this.Name = name;
            this.Icon = icon;
            this.Tag = tag;
            this.Description = description;
            this.Children = children ?? new ObservableCollection<Category>();
        }

        public string Name { get; private set; }

        public Symbol Icon { get; private set; }

        public string Tag { get; private set; }

        public string Description { get; private set; }

        public ObservableCollection<Category> Children { get; private set; }
    }
}
