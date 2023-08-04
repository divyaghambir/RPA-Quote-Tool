<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cust_Quote.aspx.cs" Inherits="RPAUKCustomerQuote.Cust_Quote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <header> 
                <h1 align="center">RPA CUSTOMER QUOTATION</h1> 
          <style>
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
  
        input[type=number] {
            -moz-appearance: textfield;
        }
    </style>
     </header> 
</head>
<body bgcolor="c2eaf6">
    <form id="form1" runat="server">
       
        <br /><br /><br /><br />
         <%--<asp:Panel ID="pnlTabs" runat="server" CssClass="TeamTabs">--%>
        <div id="divExport" runat="server" >
        <table align="center" width ="90%" id="tblQuote" runat="server">
            <br />
            <tr>
                <td><asp:Label id="lblQuoteNum" Text="Quotation Number" runat="server"/></td>
                <td ><asp:TextBox id="txtQuoteNum" Text="" runat="server"  style="width: 200px" Enabled="false" /></td>
                <td width="20%"><asp:Label id="lblCustName" Text="Customer Name" runat="server"/></td>
               <td ><asp:Textbox id="txtCustName" AutoPostBack="True" runat="server" style="width: 200px" TextMode="MultiLine" >
                 </asp:Textbox></td>
                <td width="20%"><asp:Label id="lblCustNo" Text="Customer Number" runat="server"/></td>
                <td ><asp:TextBox id="txtCustomerNumber" Text="" runat="server"  style="width: 200px" Enabled="false" /></td>
               
                 </tr>
            <tr>
                <td><asp:Label id="lblCustEmail" Text="Customer Email" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtCustEmail" Text="" runat="server" style="width: 200px"  /></td>
                 <td><asp:Label id="lblCustPhone" Text="Customer Phone" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtCustPhone" Text="" runat="server" style="width: 200px"  /></td>
                 <td><asp:Label id="lblCurrency" Text="Currency" runat="server"/></td>
                <td ><asp:TextBox id="txtCurrency" Text="" Enabled ="false" runat="server" style="width: 200px"/></td>
                <td>&nbsp;</td>
                <td >&nbsp;</td>
            </tr>
                <tr>
                <td width="20%"><asp:Label id="lblCarriageCharges" Text="Carriage Charges" runat="server"/></td>                
                   <td >
                       <asp:TextBox ID="txtCarriage" runat="server" TextMode="Number" Width="60px"></asp:TextBox>
                     

                </td>
                <td width="20%"><asp:Label id="lblProjectName" Text="Project Name" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtProjectName" Text="" runat="server" style="width: 200px" /></td>
                    <td width="20%"><asp:Label id="lblRef" runat="server" Text="Reference No"/></td>
                <td width="20%"><asp:TextBox id="txtRefNo" Text="" runat="server" style="width: 200px" /></td>
                </tr>
               
            
                <tr>
                
                <td width="20%"><asp:Label id="lblPreparedBy" Text="Prepared By" runat="server"/></td>                
                   <td ><asp:TextBox id="txtPreparedBy" Text="" runat="server"  style="width: 200px"  /></td>
                   
               <td width="20%"><asp:Label id="lblSEEmail" Text="Email" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSEEMail" style="width: 200px"  Text="" runat="server" Enabled="false" /></td>
                     <td width="20%"><asp:Label id="lblSEPhone" Text="Phone" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSEPhone" Text="" runat="server" style="width: 200px" Enabled="false" /></td>
              
                </tr>
            <tr>
                
                <td width="20%"><asp:Label id="lblSalesPerson" Text="SalesPerson" style="width: 200px"  AutoPostBack="True" runat="server"> </asp:Label>  </td>
                  <td width="20%"><asp:TextBox id="txtSalesPerson" style="width: 200px"  Text="" runat="server" /></td>
                 
              
              
                    <td width="20%"><asp:Label id="lblSPEmail" Text="Email" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSPEmail" style="width: 200px"  Text="" runat="server" /></td>
                     <td width="20%"><asp:Label id="lblSPPhone" Text="Phone" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSPPhone" Text="" runat="server" style="width: 200px"  /></td>
              
                </tr>


           <tr>
                
               <td width="20%"><asp:Label id="lblVersion" Text="Version" Visible ="false" runat="server"/></td>                
                   <td ><asp:TextBox id="txtVersion" Text="" runat="server" Visible ="false" style="width: 200px" /></td>
                <td width="20%"><asp:Label id="lblCreationDate" Text="Creation Date" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtCreationdate" style="width: 200px"  Text="" runat="server" /></td>
                     <td width="20%"><asp:Label id="lblExpirationDate" Text="Expiration Date" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtExpirationDate" Text="" runat="server" style="width: 200px" /></td>
             
           </tr>
           
             
             
           
        </table>
            </div>
            <%--</asp:Panel>--%> 
        <br /><br /><br /><br />
        <div id="divNewItem">
            <table  align="center" width="70%"><tr>  
                
                    <td width="20%">
                    <asp:Button runat="server" Text="Add New Item" ID ="btnAddNewItem" ValidationGroup="AddNewItem" OnClick="btnAddNewItem_Click"/> 
                  
                
                                               </tr>

                 <tr>
                <td width="50%"><asp:Label id="lblMessage" Text="" runat="server" ForeColor ="Red"/></td>
                </tr>
            </table>
        </div>
        <br />

         <div align="center" style="overflow-y: scroll;height: 250px; width: 100%;">  
             <asp:ScriptManager ID="ScriptManager1" runat="server">
              <Services>
                  <asp:ServiceReference Path="~/AutoCompleteTextBox.asmx" InlineScript="true" />
              </Services>
            </asp:ScriptManager>
            <asp:GridView ID="grdQuote" runat="server" CssClass="Grid"  AutoGenerateColumns="false" BackColor ="SkyBlue" AllowPaging="false" 
  PageSize="5" PagerStyle-HorizontalAlign="right" EmptyDataText="No records has been added." OnRowDeleting="grdQuote_RowDeleting" OnSelectedIndexChanged="grdQuote_SelectedIndexChanged" >

    <Columns>
     
        <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
        <asp:Label ID="lblSNo" runat="server" Text="<%# (Container.DataItemIndex)+1 %>"> 
        </asp:Label> 
        </ItemTemplate>
        </asp:TemplateField>
          <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
        
        <asp:BoundField DataField="ProductFamily" HeaderText="ProductFamily" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
         <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" ItemStyle-Width="60" />
        <asp:TemplateField HeaderText="PartNo">                    
        <ItemTemplate>
     <asp:TextBox ID="txtPartNo" Text='<%# Bind("PartNo") %>' AutoPostBack="true" runat="server" Width="100px" OnTextChanged="txtPartNo_TextChanged" ></asp:TextBox>
             <cc1:AutoCompleteExtender ID="AutoComplete" 
                              runat="server"
                              TargetControlID="txtPartNo"
                              ServicePath="~/AutoCompleteTextBox.asmx"
                              ServiceMethod="GetCompletionList"
                              MinimumPrefixLength="3" 
                              CompletionInterval="100"
                              EnableCaching="false"
                              CompletionSetCount="10"
                              FirstRowSelected="false"/>

               <asp:RequiredFieldValidator ID="RFtxtPartNumber" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Approve" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Reject" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="UpdateVersion" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Confirm" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="AddNewItem" EnableClientScript="true"></asp:RequiredFieldValidator>
            
            </ItemTemplate>
   </asp:TemplateField>
        
      
         <asp:TemplateField HeaderText="Description">                    
        <ItemTemplate>
     <asp:TextBox ID="txtDesc" Text='<%# Bind("[Description]") %>' AutoPostBack="true" runat="server" OnTextChanged="txtDesc_TextChanged"  ></asp:TextBox>
             <cc1:AutoCompleteExtender ID="AutoCompleteDesc" 
                              runat="server"
                              TargetControlID="txtDesc"
                              ServicePath="~/AutoCompleteDesc.asmx"
                              ServiceMethod="GetDescList"
                              MinimumPrefixLength="3" 
                              CompletionInterval="100"
                              EnableCaching="false"
                              CompletionSetCount="10"
                              FirstRowSelected="false"/>

               <asp:RequiredFieldValidator ID="RFtxtDesc" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Approve" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Reject" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="UpdateVersion" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Confirm" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="AddNewItem" EnableClientScript="true"></asp:RequiredFieldValidator>
            
            </ItemTemplate>
   </asp:TemplateField>




        <asp:TemplateField HeaderText="QTY">
    <ItemTemplate>
     <asp:TextBox ID="txtQTY" Text='<%# Bind("QTY") %>' AutoPostBack="true" TextMode="Number"  runat="server" Width="50px" OnTextChanged="txtQTY_TextChanged" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFtxtQTY" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
         ErrorMessage="Required Field." ValidationGroup="Submit" ></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Approve" ></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Reject" ></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="UpdateVersion" ></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Confirm" ></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" ></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="AddNewItem" ></asp:RequiredFieldValidator>
                     
    </ItemTemplate>
   </asp:TemplateField>
          <asp:BoundField DataField="AvailableQty" HeaderText="AvailableQty" ItemStyle-Width="120" />
        <asp:BoundField DataField="StockAvailability" HeaderText="StockAvailability" ItemStyle-Width="150" />
        <asp:BoundField DataField="MOQ" HeaderText="MOQ" ItemStyle-Width="120" />  
         <asp:BoundField DataField="LeadTime" HeaderText="LeadTime" ItemStyle-Width="120" />  
         <asp:BoundField DataField="SafetyStock" HeaderText="SafetyStock" ItemStyle-Width="120" />
         <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-Width="120" />
         <asp:BoundField DataField="ListPrice" HeaderText="ListPrice" ItemStyle-Width="120" />
         <asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-Width="120" />
         <asp:BoundField DataField="CostPrice" HeaderText="CostPrice" ItemStyle-Width="120" />
        <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-Width="120" />

    
    </Columns>

             

</asp:GridView>
    </div> 
         <br /><br />
        <table id="tblTotal" width="95%" runat="server"><tr>
            <td align="right" ><asp:Label id="LblGrandTotal" Text="Grand Total" runat="server" Enabled="false"/>
                <asp:TextBox id="TxtGrandTotal" Text="0" runat="server" Enabled="false"   />
            </td>
            </tr>
                </table>
       
        <br /><br />
        <table  align="right"  width="90%">
            <br /><br /><br /><br />
            <tr>
                <td width="4%"> <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height ="100px" Width="250px"/>  </td>
                <td width="4%">
                    <asp:FileUpload ID="FileUpload1" runat="server" />  
                </td>
                <td width="4%"> <asp:Button runat="server" Text="Save Quote" ID ="btnSave" ValidationGroup="Save" Visible="false" OnClick="btnSave_Click" />  </td>
                
                
                <td width="4%"> <asp:Button runat="server" Text="Submit Quote" ID ="btnSubmit" Visible="false" ValidationGroup="Submit"  OnClick="btnSubmit_Click"/> </td>
                <td width="4%"> <asp:Button runat="server" Text="Export to Excel" ID ="btnExportToExcel"  Visible ="false" OnClick="btnExportToExcel_Click" /> </td>
                <td width="4%">
                    <asp:Button runat="server" Text="Export to PDF" ID ="btnExportToPDF" Visible="false" OnClick="btnExportToPDF_Click" />   
                </td> 
              <td width="4%">
                <asp:Button runat="server" Text="Update Version" id="btnUpdateVersion" Width ="120px" ValidationGroup="UpdateVersion"   Visible ="false" OnClick="btnUpdateVersion_Click"/>  
                </td> 
                               
                
            </tr>
           
            </table>
    </form>
</body>
</html>
