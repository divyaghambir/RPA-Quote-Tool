<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cust_Dashboard.aspx.cs" Inherits="RPAUKCustomerQuote.Cust_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <header> 
                <h1 align="center">RPA QUOTATION TOOL</h1> 
            </header> 
</head>
<body bgcolor="c2eaf6">
    <form id="form1" runat="server">
         
        <table width="50%"  align="center">
            <br /><br /><br /><br />
            <tr>
                    <td>
                <asp:Button runat="server" Text="Create New Quote" ID ="btnCreateQuote" Visible ="false" OnClick="btnCreateQuote_Click" />  
               
                </td>
               
                <td>
                     <asp:Button runat="server" Text="Export to Excel" ID ="btnExport" Visible ="false" OnClick="btnExport_Click" />
                </td>
                <td>
                     <asp:Button runat="server" Text="Export Quote Details" ID ="btnQuoteDetails" Visible ="false" OnClick="btnQuoteDetails_Click" />
                </td>
               
               
                
            </tr>
        </table>
        <br /><br />

         <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        <div align="center" style="overflow-y: scroll;height: 600px; width: 100%;" >  
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdDashboard"  EventName="PageIndexChanging" />
            </Triggers>
            <ContentTemplate>

            <asp:GridView ID="grdDashboard" runat="server" CssClass="Grid" AutoGenerateColumns="false" BackColor ="SkyBlue" 

    EmptyDataText="No records has been added." OnRowCommand="grdDashboard_RowCommand">

    <Columns>
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:Button ID="btnOpen" runat="server" CausesValidation="false" CommandName="Open" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                    Text="Open" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="QuoteNo" HeaderText="Quotation Number" ItemStyle-Width="120" />
        <asp:BoundField DataField="SalesEngineer" HeaderText="Sales Engineer" ItemStyle-Width="120" />
        <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" ItemStyle-Width="120" />
        <asp:BoundField DataField="CustomerNo" HeaderText="Customer #" ItemStyle-Width="120" />
         <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="120" />
        <asp:BoundField DataField="CreationDate" HeaderText="Offer Creation Date " ItemStyle-Width="120" />
        <asp:BoundField DataField="ExpirationDate" HeaderText="Offer Expiration Date" ItemStyle-Width="120" />        
        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="120" />
        <asp:BoundField DataField="GrandTotal" HeaderText="Total Amount" ItemStyle-Width="120" />
       
        


    </Columns>

</asp:GridView>
                </ContentTemplate>
        </asp:UpdatePanel>

    </div> 
         <br /><br /><br /><br /> <br /><br /><br /><br />

        <%--<div>
            <asp:Button runat="server" Text="Button" OnClick="Unnamed1_Click" />
        </div>--%>
    </form>
</body>
</html>
