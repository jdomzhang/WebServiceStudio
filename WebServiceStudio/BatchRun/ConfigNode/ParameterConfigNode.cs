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
	




namespace IBS.Utilities.ASMWSTester.BatchRun.ConfigNode
{
    public class ParameterConfigNode : XmlConfigNodeBase
    {
        public ParameterConfigNode()
        {
            nodeName = "parameter";
        }

        private string xpath;
        private string valuefieldname;

        public string Xpath
        {
            get { return xpath; }
            set { xpath = value; }
        }

        public string Valuefieldname
        {
            get { return valuefieldname; }
            set { valuefieldname = value; }
        }
    }
}
