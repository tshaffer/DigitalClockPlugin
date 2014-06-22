using System.Windows.Controls;

using HTMLPluginContract;
using System.Windows;
using System;
using System.Diagnostics;
using System.Xml;
using System.Reflection;

namespace DigitalClockPlugin
{
    class DigitalClock : IHTMLPlugin
    {
        // for HTML5PlaylistItem compatibility
        public string HTMLSiteName { get; set; }
        public bool EnableExternalData { get; set; }
        public bool EnableMouseEvents { get; set; }
        public bool DisplayCursor { get; set; }
        public string TimeOnScreen { get; set; }

        // specific to digital clock
        private string _format = "hh:mm";
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }

        public string Font { get; set; }

        private string _color = "FF000080";
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        private string _size = "-1";
        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public string Caption { get; set; }
        public string OffsetX { get; set; }
        public string  OffsetY { get; set; }
        public string  TimeZone { get; set; }
        public string  Alignment { get; set; }
        public string  Opacity { get; set; }
        public string  Width { get; set; }
        public string Height { get; set; }

        #region IPlugin Members

        private string _name = "DigitalClock";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public Image GetIconImage()
        {
            Image iconImage = null;

            try
            {
                iconImage = (Image)Application.Current.FindResource("icon_mediaSmall_clock");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ProgramError", "DigitalClock", MessageBoxButton.OK, MessageBoxImage.Error);
                Trace.WriteLine("Exception in DigitalClock.cs when trying to read resources");
                Trace.WriteLine("Error is " + ex.Message);
            }

            return iconImage;
        }

        public Image GetThumbnail()
        {
            Image thumbnail = null;

            try
            {
                thumbnail = (Image)Application.Current.FindResource("icon_mediaLarge_clock");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ProgramError", "DigitalClock", MessageBoxButton.OK, MessageBoxImage.Error);
                Trace.WriteLine("Exception in DigitalClock.cs when trying to read resources");
                Trace.WriteLine("Error is " + ex.Message);
            }

            return thumbnail;
        }

        public bool IsEqual(IHTMLPlugin plugin)
        {
            DigitalClock digitalClock = (DigitalClock)plugin;

            bool digitalClocksEqual = (digitalClock.HTMLSiteName == this.HTMLSiteName) &&
                (digitalClock.EnableExternalData == this.EnableExternalData) &&
                (digitalClock.DisplayCursor == this.DisplayCursor) &&
                (digitalClock.TimeOnScreen == this.TimeOnScreen) &&
                (digitalClock.Format == this.Format) &&
                (digitalClock.Font == this.Font) &&
                (digitalClock.Color == this.Color) &&
                (digitalClock.Size == this.Size) &&
                (digitalClock.Caption == this.Caption) &&
                (digitalClock.OffsetX == this.OffsetX) &&
                (digitalClock.OffsetY == this.OffsetY) &&
                (digitalClock.TimeZone == this.TimeZone) &&
                (digitalClock.Alignment == this.Alignment) &&
                (digitalClock.Opacity == this.Opacity) &&
                (digitalClock.Width == this.Width) &&
                (digitalClock.Height == this.Height);
            
            return digitalClocksEqual;
        }

        public void CopyMembers(IHTMLPlugin plugin)
        {
            if (plugin is DigitalClock)
            {
                DigitalClock dc = plugin as DigitalClock;
                HTMLSiteName = dc.HTMLSiteName;
                EnableExternalData = dc.EnableExternalData;
                DisplayCursor = dc.DisplayCursor;
                TimeOnScreen = dc.TimeOnScreen;

                Format = dc.Format;
                Font = dc.Font;
                Color = dc.Color;
                Size = dc.Size;
                Caption = dc.Caption;
                OffsetX = dc.OffsetX;
                OffsetY = dc.OffsetY;
                TimeZone = dc.TimeZone;
                Alignment = dc.Alignment;
                Opacity = dc.Opacity;
                Width = dc.Width;
                Height = dc.Height;
            }
        }

        public void WriteToXml(XmlTextWriter writer, bool publish)
        {
            // temporary - initialize values here
            HTMLSiteName = "DigitalClock";
            EnableExternalData = true;
            EnableMouseEvents = false;
            DisplayCursor = false;
            TimeOnScreen = "0";

            //Format = "hh:mm:ss tt";
            // Font = "";
            // Color = "000080";
            //Size = "-1";
            Caption = "";
            OffsetX = "0";
            OffsetY = "0";
            TimeZone = "300";
            Alignment = "center";
            Opacity = "1";
            Width = "516px";
            Height = "199px";

            writer.WriteStartElement("html5Item");

            writer.WriteElementString("name", "DigitalClock");
            writer.WriteElementString("htmlSiteName", HTMLSiteName);
            writer.WriteElementString("enableExternalData", EnableExternalData.ToString());
            writer.WriteElementString("enableMouseEvents", EnableMouseEvents.ToString());
            writer.WriteElementString("displayCursor", DisplayCursor.ToString());
            writer.WriteElementString("timeOnScreen", TimeOnScreen);

            //foreach (string customFont in CustomFonts)
            //{
            //    if (publish)
            //    {
            //        writer.WriteElementString("customFont", System.IO.Path.GetFileName(customFont));
            //    }
            //    else
            //    {
            //        writer.WriteElementString("customFont", customFont);
            //    }
            //}

            if (publish)
            {
                //string queryString = "format=hh:mm:ss tt;color=000080;size=-1;caption=;offsetx=0;offsety=0;timezone=300;alignment=center;opacity=1;width=516px;height=199px";
                string queryString = "format=" + Format + ";";
//                queryString += ";color=000080;";
                //queryString += ";color=800000;";
                string c = Color.Substring(2);
                queryString += "color=" + c + ";";
                queryString += "size=" + Size;
                queryString += ";caption=;offsetx=0;offsety=0;timezone=300;alignment=center;opacity=1;width=516px;height=199px";
                string escapedQueryString = System.Uri.EscapeDataString(queryString); // for params
                writer.WriteElementString("queryString", "#?format=" + escapedQueryString);
            }
            else
            {
                writer.WriteElementString("format", Format);
                writer.WriteElementString("color", Color);
                writer.WriteElementString("size", Size);
                writer.WriteElementString("caption", Caption);
                writer.WriteElementString("offsetX", OffsetX);
                writer.WriteElementString("offsetY", OffsetY);
                writer.WriteElementString("timeZone", TimeZone);
                writer.WriteElementString("alignment", Alignment);
                writer.WriteElementString("opacity", Opacity);
                writer.WriteElementString("width", Width);
                writer.WriteElementString("height", Height);
            }

            writer.WriteEndElement(); // html5Item

        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read() && (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Whitespace))
            {
                switch (reader.LocalName)
                {
                    case "name":
                        Name = reader.ReadString();
                        break;
                    case "htmlSiteName":
                        HTMLSiteName = reader.ReadString();
                        break;
                    case "enableExternalData":
                        EnableExternalData = Convert.ToBoolean(reader.ReadString());
                        break;
                    case "enableMouseEvents":
                        EnableMouseEvents = Convert.ToBoolean(reader.ReadString());
                        break;
                    case "displayCursor":
                        DisplayCursor = Convert.ToBoolean(reader.ReadString());
                        break;
                    case "timeOnScreen":
                        TimeOnScreen = reader.ReadString();
                        break;

                    case "format":
                        Format = reader.ReadString();
                        break;
                    case "color":
                        Color = reader.ReadString();
                        break;
                    case "size":
                        Size = reader.ReadString();
                        break;
                    case "caption":
                        Caption = reader.ReadString();
                        break;
                    case "offsetX":
                        OffsetX = reader.ReadString();
                        break;
                    case "offsetY":
                        OffsetY = reader.ReadString();
                        break;
                    case "timeZone":
                        TimeZone = reader.ReadString();
                        break;
                    case "alignment":
                        Alignment = reader.ReadString();
                        break;
                    case "opacity":
                        Opacity = reader.ReadString();
                        break;
                    case "width":
                        Width = reader.ReadString();
                        break;
                    case "height":
                        Height = reader.ReadString();
                        break;
                }
            }
        }

        public void ReadXmlItem(XmlReader reader)
        {
            switch (reader.LocalName)
            {
                case "htmlSiteName":
                    HTMLSiteName = reader.ReadString();
                    break;
                case "enableExternalData":
                    EnableExternalData = Convert.ToBoolean(reader.ReadString());
                    break;
                case "enableMouseEvents":
                    EnableMouseEvents = Convert.ToBoolean(reader.ReadString());
                    break;
                case "displayCursor":
                    DisplayCursor = Convert.ToBoolean(reader.ReadString());
                    break;
                case "timeOnScreen":
                    TimeOnScreen = reader.ReadString();
                    break;

                case "format":
                    Format = reader.ReadString();
                    break;
                case "color":
                    Color = reader.ReadString();
                    break;
                case "size":
                    Size = reader.ReadString();
                    break;
                case "caption":
                    Caption = reader.ReadString();
                    break;
                case "offsetX":
                    OffsetX = reader.ReadString();
                    break;
                case "offsetY":
                    OffsetY = reader.ReadString();
                    break;
                case "timeZone":
                    TimeZone = reader.ReadString();
                    break;
                case "alignment":
                    Alignment = reader.ReadString();
                    break;
                case "opacity":
                    Opacity = reader.ReadString();
                    break;
                case "width":
                    Width = reader.ReadString();
                    break;
                case "height":
                    Height = reader.ReadString();
                    break;
            }
        }

        public bool ExecuteEditItem()
        {
            EditParametersDlg editParametersDlg = new EditParametersDlg();

            editParametersDlg.Format = Format;
            editParametersDlg.Color = Color;
            editParametersDlg.Size = Size;

            if (editParametersDlg.ShowDialog() == true)
            {
                Format = editParametersDlg.Format;
                Color = editParametersDlg.Color;
                Size = editParametersDlg.Size;
                return true;
            }
            return false;
        }

        #endregion
    }
}
