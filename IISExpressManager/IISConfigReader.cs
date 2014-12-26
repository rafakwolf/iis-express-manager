using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace IISExpressManager
{
    internal class IISConfigReader
    {

        private static List<IISSites> _iisSites;

        internal static List<IISSites> ReadXmlFromConfig(IISExpressConfiguration iisExConfig)
        {
            _iisSites = new List<IISSites>();

            if (!iisExConfig.CheckIISExpressConfigExistence()) return null;

            string contents = File.ReadAllText(iisExConfig.IISExpressConfigAddress);

            var document = new XmlDocument();
            document.LoadXml(contents);
            XmlNodeList siteList = document.GetElementsByTagName("site");
            XmlNodeList bindingNodes = document.SelectNodes("/configuration/system.applicationHost/sites/site/bindings");
            XmlNodeList physicalPaths = document.GetElementsByTagName("virtualDirectory");
            int counter = 0;
            foreach (var node in siteList)
            {
                var xmlElement = (XmlElement)node;
                if (bindingNodes != null)
                {
                    var xmlNode = bindingNodes.Item(counter);
                    var pathNode = physicalPaths.Item(counter);

                    if (xmlNode != null)
                    {
                        var portNumber = FindPort(xmlNode.InnerXml);

                        if (pathNode != null)
                        {
                            if (pathNode.Attributes != null)
                            {
                                var physicalPath = pathNode.Attributes["physicalPath"].Value;

                                _iisSites.Add(new IISSites(xmlElement.Attributes["name"].Value,
                                    xmlElement.Attributes["id"].Value,
                                    portNumber,
                                    physicalPath));
                            }
                        }
                    }

                }
                counter++;
            }

            return _iisSites;
        }

        private static string FindPort(string innerXmlString)
        {
            int indexOfLastColon;
            string portNumber;
            int portIndex = innerXmlString.IndexOf("=\":", StringComparison.Ordinal);

            if (portIndex == -1)
            {
                portIndex = innerXmlString.IndexOf("*", StringComparison.Ordinal);
                portNumber = innerXmlString.Substring(portIndex + 2);
                indexOfLastColon = portNumber.IndexOf(":", StringComparison.Ordinal);

                portNumber = portNumber.Substring(0, indexOfLastColon);
                return portNumber;
            }
            portNumber = innerXmlString.Substring(portIndex + 3);
            indexOfLastColon = portNumber.IndexOf(":", StringComparison.Ordinal);

            portNumber = portNumber.Substring(0, indexOfLastColon);
            return portNumber;

        }
    }
}