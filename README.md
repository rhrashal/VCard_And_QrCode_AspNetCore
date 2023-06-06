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
// text different line
 var contactInfo = new StringBuilder();
    contactInfo.Append("Property of: NRB Bank Ltd \n");
    contactInfo.Append("Location: " + branch + " \n");
    contactInfo.Append("Type of Assets: " + assetType + " \n");
    contactInfo.Append("Asset Name: " + assetName + " \n");
    contactInfo.Append("Vendor Name: " + vendor + " \n");
    contactInfo.Append("Brand Name: " + brand + " \n");
    contactInfo.Append("Quantity: " + qty + " \n");
    contactInfo.Append("Purchase Date: " + date);
// for vcard text    
private string GenerateVCardString(string name, string email, string address)
{
    StringBuilder vCardBuilder = new StringBuilder();
    vCardBuilder.AppendLine("BEGIN:VCARD");
    vCardBuilder.AppendLine("VERSION:2.1");
    vCardBuilder.AppendLine("N:" + name);
    vCardBuilder.AppendLine("EMAIL:" + email);
    vCardBuilder.AppendLine("ADR:" + address);
    vCardBuilder.AppendLine("END:VCARD");

    return vCardBuilder.ToString();
}
```
