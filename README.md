# VCard_And_QrCode_AspNetCore

### install package : MixERP.Net.VCards

### install package : System.Drawing.Common

### install package : ZXing.Net.Bindings.Windows.Compatibility






### aspx QR Code 
```c#
protected void gvBarcodeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the data for the current row
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string qrText = rowView["QrText"].ToString();


                System.Web.UI.WebControls.Image qrCodeImage = e.Row.FindControl("QRCodeImage") as System.Web.UI.WebControls.Image;

                if (qrCodeImage != null)
                {
                    qrCodeImage.ImageUrl = qrCodeGen(qrText);
                }

            }

        }

        private string qrCodeGen(string qrString)
        {
            string imageFileName = "klc_qr_code.png";

            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 150,
                Height = 150
            };

            BarcodeWriter writer = new BarcodeWriter()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };

            Bitmap qrCodeBitmap = writer.Write(qrString);
            //qrCodeBitmap.Save(imageFileName);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                qrCodeBitmap.Save(memoryStream, ImageFormat.Png);
                byte[] bitmapBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(bitmapBytes);
                return "data:image/png;base64," + base64String;
            }
        }
```
