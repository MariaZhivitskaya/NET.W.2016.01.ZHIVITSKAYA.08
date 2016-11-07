using System;
using System.Globalization;

namespace Task1
{
    public interface IComponent : ICloneable
    {
        dynamic Info { get; set; }

        string Format { get; set; }

        string Operation();
    }

    public class Component : IComponent
    {
        public dynamic Info { get; set; }

        public string Format { get; set; }

        public string Operation()
        {
            return "Customer record: ";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    
    public class DecoratorName : IComponent, ICloneable
    {
        readonly IComponent iComponent;

        public DecoratorName(IComponent iComponent)
        {
            this.iComponent = iComponent;
        }
       
        public dynamic Info
        {
            get { return iComponent.Info; }
            set { iComponent.Info = value; }
        }

        public string Format { get; set; }

        public string Operation()
        {
            var s = iComponent.Operation();
            s += Info.ToString() + " ";

            return s;
        }

        public object Clone()
        {
            var dn = new DecoratorName((IComponent)iComponent.Clone());

            return dn;
        }
    }

    public class DecoratorContactPhone : IComponent
    {
        readonly IComponent iComponent;

        public DecoratorContactPhone(IComponent iComponent)
        {
            this.iComponent = iComponent;
        }

        public dynamic Info
        {
            get { return iComponent.Info; }
            set { iComponent.Info = value; }
        }

        public string Format { get; set; }

        public string Operation()
        {
            var s = iComponent.Operation();
            s += Info.ToString() + " ";

            return s;
        }

        public object Clone()
        {
            var dcp = new DecoratorContactPhone((IComponent)iComponent.Clone());

            return dcp;
        }
    }

    public class DecoratorRevenue : IComponent
    {
        readonly IComponent iComponent;

        public DecoratorRevenue(IComponent iComponent, string format = "N2")
        {
            this.iComponent = iComponent;
            Format = format;
        }

        public dynamic Info
        {
            get { return iComponent.Info; }
            set { iComponent.Info = value; }
        }

        public string Format { get; set; }

        public string Operation()
        {
            var s = iComponent.Operation();
            s += Info.ToString(Format) + " ";

            return s;
        }

        public object Clone()
        {
            var dr = new DecoratorRevenue((IComponent)iComponent.Clone());

            return dr;
        }
    }

    public class Customer : IFormattable
    {
        private string name;
        private string contactPhone;
        private decimal revenue;

        public string Name
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Null or empty data!");

                name = value;
            }
            get { return name; }
        } 

        public string ContactPhone
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Null or empty data!");

                contactPhone = value;
            }
            get { return contactPhone; }
        }

        public decimal Revenue
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException
                        ("Revenue can't be less or equal than 0!");

                revenue = value;
            }
            get { return revenue; }
        }

        public Customer(string name, string contactPhone, decimal revenue)
        {
            Name = name;
            ContactPhone = contactPhone;
            Revenue = revenue;
        }

        public override string ToString()
        {
            return Name + " " + ContactPhone + " " +
                   Revenue.ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return Name + " " + ContactPhone + " " +
                   Revenue.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "N":
                    return Name + " " + ContactPhone + " " +
                        Revenue.ToString("N", formatProvider);
                case "D":
                    return Name + " " + ContactPhone + " " +
                        Revenue.ToString("D", formatProvider);
                default:
                    throw new FormatException($"The {format} format string is not supported!");
            }
        }
    }
}
