<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cust_Login.aspx.cs" Inherits="RPAUKCustomerQuote.Cust_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UK Customer Quote</title>
      <table align="right"> <tr><td><asp:Image id="Image1" runat="server" ImageUrl="~\images\logo.png" ></asp:Image></td></tr></table>
    <header> 
                <h1 align="center">RPA CUSTOMER QUOTATION</h1> 
            </header> 
</head>
<body bgcolor="c2eaf6">
     <form id="form1" runat="server">
       
         
         <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br />
         <table width="30%"  align="center">
            <tr>
               
                <td align="center"><asp:Label id="lblUserName" Text="User Name" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtUserName" Text="" runat="server"  style="width: 200px"  />
                    <asp:RequiredFieldValidator ID="RFtxtUserName" runat="server" ControlToValidate="txtUserName" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                </td></tr>  
             <tr>
                <td align="center"><asp:Label id="lblPassword" Text="Password" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtPassword" Text="" runat="server"  style="width: 200px" TextMode="Password"  />
                    <asp:RequiredFieldValidator ID="RFtxtPassword" runat="server" ControlToValidate="txtPassword" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                </td></tr>  
             <tr><td /></td> <td><asp:Label id="lblmessage" Text="" runat="server" ForeColor ="Red" Enabled="false"/></td></tr>
             <tr><td /></td></tr>
             <tr><td width="100%" align ="right"><asp:Button runat="server" ValidationGroup="Submit" Text="Login" ID ="btnLogin" OnClick="btnLogin_Click" /></td></tr>
         </table>
    </form>
</body>
</html>
