	//WebServiceStudio Application to test WCF web services.

	//Copyright (C) 2012  Irdeto Customer Care And Billing

	//This program is free software: you can redistribute it and/or modify
	//it under the terms of the GNU General Public License as published by
	//the Free Software Foundation, either version 3 of the License, or
	//(at your option) any later version.

	//This program is distributed in the hope that it will be useful,
	//but WITHOUT ANY WARRANTY; without even the implied warranty of
	//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	//GNU General Public License for more details.

	//You should have received a copy of the GNU General Public License
	//along with this program.  If not, see http://www.gnu.org/licenses/

	//Web service Studio has been modifided to understand more complex input types
	//such as Iredeto's Customer Care input type of Base Query Request. 
	




using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace IBS.Utilities.ASMWSTester
{
    internal class DynamicConverter : TypeConverter
    {
        static DynamicConverter()
        {
            converterTable = null;
            typeNotFoundMessage =
                "ProxyPropertiesType {0} specified in IBS.Utilities.ASMWSTester.exe.options is not found";
            converterTable = new Hashtable();
            converterTable[typeof (bool)] = Activator.CreateInstance(typeof (BooleanConverter));
            converterTable[typeof (byte)] = Activator.CreateInstance(typeof (ByteConverter));
            converterTable[typeof (sbyte)] = Activator.CreateInstance(typeof (SByteConverter));
            converterTable[typeof (char)] = Activator.CreateInstance(typeof (CharConverter));
            converterTable[typeof (double)] = Activator.CreateInstance(typeof (DoubleConverter));
            converterTable[typeof (string)] = Activator.CreateInstance(typeof (StringConverter));
            converterTable[typeof (int)] = Activator.CreateInstance(typeof (Int32Converter));
            converterTable[typeof (short)] = Activator.CreateInstance(typeof (Int16Converter));
            converterTable[typeof (long)] = Activator.CreateInstance(typeof (Int64Converter));
            converterTable[typeof (float)] = Activator.CreateInstance(typeof (SingleConverter));
            converterTable[typeof (ushort)] = Activator.CreateInstance(typeof (UInt16Converter));
            converterTable[typeof (uint)] = Activator.CreateInstance(typeof (UInt32Converter));
            converterTable[typeof (ulong)] = Activator.CreateInstance(typeof (UInt64Converter));
            converterTable[typeof (object)] = Activator.CreateInstance(typeof (ExpandableObjectConverter));
            converterTable[typeof (void)] = Activator.CreateInstance(typeof (TypeConverter));
            converterTable[typeof (CultureInfo)] = Activator.CreateInstance(typeof (CultureInfoConverter));
            converterTable[typeof (DateTime)] = Activator.CreateInstance(typeof (DateTimeConverter));
            converterTable[typeof (decimal)] = Activator.CreateInstance(typeof (DecimalConverter));
            converterTable[typeof (TimeSpan)] = Activator.CreateInstance(typeof (TimeSpanConverter));
            converterTable[typeof (Guid)] = Activator.CreateInstance(typeof (GuidConverter));
            foreach (CustomHandler handler1 in Configuration.MasterConfig.TypeConverters)
            {
                Type type1 = Type.GetType(handler1.TypeName);
                if (type1 == null)
                {
                    MainForm.ShowMessage(typeof (DynamicConverter), MessageType.Warning,
                                         string.Format(typeNotFoundMessage, handler1.TypeName));
                }
                else
                {
                    Type type2 = Type.GetType(handler1.Handler);
                    if (type2 == null)
                    {
                        MainForm.ShowMessage(typeof (DynamicConverter), MessageType.Warning,
                                             string.Format(typeNotFoundMessage, handler1.Handler));
                    }
                    else
                    {
                        converterTable[type1] = Activator.CreateInstance(type2);
                    }
                }
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return GetConverter(context).CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return GetConverter(context).CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return GetConverter(context).ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            return GetConverter(context).ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return GetConverter(context).CreateInstance(context, propertyValues);
        }

        private static Type GetContainedType(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                TreeNodeProperty property1 = context.Instance as TreeNodeProperty;
                if (property1 != null)
                {
                    return property1.Type;
                }
            }
            return null;
        }

        private static TypeConverter GetConverter(ITypeDescriptorContext context)
        {
            object obj1 = null;
            Type type1 = GetContainedType(context);
            if (type1 != null)
            {
                obj1 = converterTable[type1];
            }
            if (obj1 == null)
            {
                if ((type1 != null) && type1.IsEnum)
                {
                    obj1 = converterTable[type1] = new EnumConverter(type1);
                }
                else
                {
                    obj1 = converterTable[typeof (object)];
                }
            }
            return (obj1 as TypeConverter);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetCreateInstanceSupported(context);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value,
                                                                   Attribute[] attributes)
        {
            return GetConverter(context).GetProperties(context, value, attributes);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetPropertiesSupported(context);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetStandardValues(context);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetStandardValuesExclusive(context);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return GetConverter(context).GetStandardValuesSupported(context);
        }

        public static bool IsConverterDefined(Type type)
        {
            return (converterTable[type] != null);
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return GetConverter(context).IsValid(context, value);
        }


        private static Hashtable converterTable;
        private static string typeNotFoundMessage;
    }
}
