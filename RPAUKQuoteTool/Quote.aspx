<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quote.aspx.cs" enableEventValidation ="false" Inherits="RPADubaiQuoteTool.Quote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
         .auto-style1 {
             width: 14%;
         }
         .auto-style2 {
             width: 23%;
         }
     </style>
     <header> 
                <h1 align="center">RPA QUOTATION TOOL</h1> 
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
                <td class="auto-style1"><asp:Label id="lblQuoteNum" Text="Quotation Number" runat="server"/></td>
                <td class="auto-style2"><asp:TextBox id="txtQuoteNum" Text="" runat="server"  Enabled="false" /></td>
                <td><asp:Label id="lblCustomer" Text="Customer" runat="server"/></td>
               <td><asp:TextBox id="txtCustomerName" Text="" runat="server" Enabled="false" Width="200px" /></td>
                <td><asp:Label id="lblCustNo" Text="Customer Number" runat="server"/></td>
                <td><asp:TextBox id="txtCustomerNumber" Text="" runat="server"  Enabled="false" /></td>
                <td><asp:Label id="lblCustName" Text="Customer Name" runat="server" Visible="false"/></td>
                <td><asp:Textbox id="txtCustName" AutoPostBack="True" runat="server" Visible="false" TextMode="MultiLine" >
                 </asp:Textbox></td>
                 </tr>
            <tr>
                <td class="auto-style1"><asp:Label id="lblCustBranch" Text="Customer Branch" runat="server" Enabled="false"/></td>
                <td class="auto-style2" ><asp:DropDownList id="drpCustBranch" runat="server" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="drpCustBranch_SelectedIndexChanged"  >
                    <asp:ListItem Value="0" Text="Select" />
                    </asp:DropDownList></td>
                 <td><asp:Label id="lblCustEmail" Text="Customer Email" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtCustEmail" Text="" runat="server"  /></td>
                 <td><asp:Label id="lblCustPhone" Text="Customer Phone" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtCustPhone" Text="" runat="server"   /></td>
                <td><asp:Label id="lblSundryBranch" Text="Sundry Branch" runat="server" Visible="false"/></td>
                <td ><asp:TextBox id="txtSundryBranch" Text="" TextMode="MultiLine" runat="server" Visible="false"  /></td>
            </tr>
                <tr>
                <td class="auto-style1"><asp:Label id="lblCarriageCharges" Text="Carriage Charges" runat="server"/></td>                
                   <td class="auto-style2" ><asp:DropDownList id="drpCarriageCharges"  AutoPostBack="True" runat="server"  Width="200px" OnSelectedIndexChanged="drpCarriageCharges_SelectedIndexChanged">
                  <asp:ListItem Selected="True" Value="-1"> Select </asp:ListItem>
                  
                    </asp:DropDownList>
                       <asp:TextBox ID="txtCarriage" runat="server" TextMode="Number" Width="60px"></asp:TextBox>
                     

                </td>
                <td><asp:Label id="lblProjectName" Text="Project Name" runat="server"/></td>
                <td><asp:TextBox id="txtProjectName" Text="" runat="server"  /></td>
                    <td><asp:Label id="lblOppurtunityId" Text="Oppurtunity Id" runat="server"/></td>
                <td><asp:TextBox id="txtOppurtunityId" Text="" runat="server"  /></td>
                </tr>
                <tr>                
               
                    
                   
                    <td class="auto-style1"><asp:Label id="lblCurrency" Text="Currency" runat="server"/></td>
                <td class="auto-style2"><asp:TextBox id="txtCurrency" Text="" Enabled ="false" runat="server" /></td>
                </tr>
            
                <tr>
                
                <td class="auto-style1"><asp:Label id="lblPreparedBy" Text="Prepared By" runat="server"/></td>                
                   <td class="auto-style2" ><asp:TextBox id="txtPreparedBy" Text="" runat="server"  style="width: 200px"  /></td>
                   
               <td><asp:Label id="lblSEEmail" Text="Email" runat="server"/></td>
                <td><asp:TextBox id="txtSEEMail"  Text="" runat="server" Enabled="false" /></td>
                     <td><asp:Label id="lblSEPhone" Text="Phone" runat="server"/></td>
                <td><asp:TextBox id="txtSEPhone" Text="" runat="server" Enabled="false" /></td>
              
                </tr>
            <tr>
                
                <td class="auto-style1"><asp:Label id="lblSalesPerson" Text="SalesPerson"  AutoPostBack="True" runat="server"> </asp:Label>  </td>
                  <td class="auto-style2"><asp:TextBox id="txtSalesPerson"  Text="" runat="server" /></td>
                 
              
              
                    <td><asp:Label id="lblSPEmail" Text="Email" runat="server"/></td>
                <td><asp:TextBox id="txtSPEmail"  Text="" runat="server" /></td>
                     <td><asp:Label id="lblSPPhone" Text="Phone" runat="server"/></td>
                <td><asp:TextBox id="txtSPPhone" Text="" runat="server"  /></td>
              
                </tr>


           <tr>
                
               <td class="auto-style1"><asp:Label id="lblVersion" Text="Version" Visible ="false" runat="server"/></td>                
                   <td class="auto-style2" ><asp:TextBox id="txtVersion" Text="" runat="server" Visible ="false" /></td>
                <td><asp:Label id="lblCreationDate" Text="Creation Date" runat="server"/></td>
                <td><asp:TextBox id="txtCreationdate"  Text="" runat="server" /></td>
                     <td><asp:Label id="lblExpirationDate" Text="Expiration Date" runat="server"/></td>
                <td><asp:TextBox id="txtExpirationDate" Text="" runat="server"  /></td>
               <td colspan="2"><asp:CheckBox runat="server" ID="chkPerforma" Text="Creating Proforma Invoice" AutoPostBack="true" OnCheckedChanged="chkPerforma_CheckedChanged" /></td>
               <td></td>
           </tr>
           
             <tr>
                 <td class="auto-style1"><asp:Label id="lblPerforma" Font-Bold="true" Text="" runat="server" ForeColor ="Blue"/></td>
                 
             </tr>
              <tr>
                  <td class="auto-style1"><asp:CheckBox ID="chkExportCustomer" runat="server" AutoPostBack="true" Text="Export Customer" OnCheckedChanged="chkExportCustomer_CheckedChanged" /></td>
                <td class="auto-style2"><asp:Label id="lblVAT" Text="VAT No" runat="server" Visible="false" Width="100px"/>
                   <asp:TextBox id="txtVAT" Font-Bold="true"  runat="server" Visible="false"/></td>
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
       <%-- <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="SendMail"
                    Text="Open" />
            </ItemTemplate>
        </asp:TemplateField>--%>
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
         <asp:BoundField DataField="AvailableQty" HeaderText="AvailableQty" ItemStyle-Width="120" >        
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="StockAvailability" HeaderText="StockAvailability" ItemStyle-Width="120" >        
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="MOQ" HeaderText="MOQ" ItemStyle-Width="120" >        
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
      <asp:TemplateField HeaderText="LeadTime">
             <ItemTemplate>
         <asp:TextBox ID="txtLeadTime" Text='<%# Bind("LeadTime") %>' AutoPostBack="true" Width="50px" TextMode="Number" runat="server" ></asp:TextBox>
           
               
        </ItemTemplate>
             </asp:TemplateField>
        <asp:BoundField DataField="SafetyStock" HeaderText="SafetyStock" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
         <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-Width="120" />
          <asp:BoundField DataField="ListPrice" HeaderText="ListPrice" ItemStyle-Width="120" />
         <asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-Width="120" />
        <asp:TemplateField HeaderText="UnitPrice">
             <ItemTemplate>
         <asp:TextBox ID="txtUnitPrice" Text='<%# Bind("UnitPrice") %>' AutoPostBack="true"  Width="50px"  runat="server" OnTextChanged="txtUnitPrice_TextChanged" ></asp:TextBox>
           
               
        </ItemTemplate>
             </asp:TemplateField>

        <asp:TemplateField HeaderText="AdditionalDiscount">
        <ItemTemplate>
        <asp:TextBox ID="txtDiscount" Text='<%#Bind("AdditionalDiscount") %>'  AutoPostBack="true" runat="server"  Width="50px" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
        </ItemTemplate>
       </asp:TemplateField>
        <asp:BoundField DataField="Unit Price After Extra Discount" HeaderText="Unit Price After Extra Discount" ItemStyle-Width="120" >        
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="Total Price After Extra Discount" HeaderText="Total Price After Extra Discount" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="GM" HeaderText="GM" ItemStyle-Width="120" >
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="CostPrice" HeaderText="CostPrice" ItemStyle-Width="120" Visible="true" >
<ItemStyle Width="120px"></ItemStyle>
        </asp:BoundField>
        
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="SelectCheckBox" Visible ="false" runat="server" AutoPostBack="true"  OnCheckedChanged="SelectCheckBox_OnCheckedChanged"/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

                
                <%--<PagerSettings PageButtonCount="5" />
                <PagerStyle HorizontalAlign="Right" />--%>

</asp:GridView>
    </div> 
         <br /><br />
        <table id="tblTotal" width="95%" runat="server"><tr>
            <td align="right" ><asp:Label id="LblGrandTotal" Text="Grand Total" runat="server" Enabled="false"/>
                <asp:TextBox id="TxtGrandTotal" Text="0" runat="server" Enabled="false"   />
          <asp:Label id="lblTotalGM" Text="Total GM" runat="server" Enabled="false"/>
                <asp:TextBox id="txtTotalGM" Text="0" runat="server" Enabled="false"   />
            </td>
            </tr>
                </table>
       
        <br /><br />
        <table  align="right"  width="90%">
            <br /><br /><br /><br />
            <tr>
                <td width="4%"> <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height ="100px" Width="250px"/>  </td>
                <td width="4%">
                <asp:Button runat="server" Text="Approve" Width ="120px" ID="btnApprove" ValidationGroup="Approve"  OnClick="btnApprove_Click"  Visible ="false"/>  
                </td>
                <td width="4%"><asp:Button runat="server" Width ="120px" Text="Reject"  ID ="btnReject" ValidationGroup="Reject"   Visible ="false" OnClick="btnReject_Click" style="height: 26px" /></td>
                
                
                <td width="4%"> <asp:Button runat="server" Text="Export to Excel" ID ="btnExportToExcel"  Visible ="false" OnClick="btnExportToExcel_Click" /> </td>
                <td width="4%"> <asp:Button runat="server" Text="Export to PDF" ID ="btnExportToPDF"  Visible ="false" OnClick="btnExportToPDF_Click" /> </td>
                <td width="4%">
                <asp:Button runat="server" Text="Update Version" id="btnUpdateVersion" Width ="120px" ValidationGroup="UpdateVersion"   Visible ="false" OnClick="btnUpdateVersion_Click"/>  
                </td> 
                <td><asp:DropDownList ID="drpStatus" runat="server" Visible="false">
                    <asp:ListItem>CONFIRMED</asp:ListItem>
                    <asp:ListItem>POSTPONED</asp:ListItem>
                    <asp:ListItem>CUSTOMER LOST</asp:ListItem>
                    <asp:ListItem>LOST-PRICE</asp:ListItem>
                    <asp:ListItem>LOST-STOCK</asp:ListItem>
                    <asp:ListItem>LOST-PRODUCT</asp:ListItem>
                    <asp:ListItem>CONFIRMED(Richard)</asp:ListItem>
                    <asp:ListItem>PENDING ACTIVE(Richard)</asp:ListItem>
                    <asp:ListItem>LOST PRICE(Richard)</asp:ListItem>
                    <asp:ListItem>LOST AVAILABILITY(Richard)</asp:ListItem>
                    <asp:ListItem>LOST CUSTOMER(Richard)</asp:ListItem>
                    <asp:ListItem>CONFIRMED(Juliana)</asp:ListItem>
                    <asp:ListItem>PENDING ACTIVE(Juliana)</asp:ListItem>
                    <asp:ListItem>LOST PRICE(Juliana)</asp:ListItem>
                    <asp:ListItem>LOST AVAILABILITY(Juliana)</asp:ListItem>
                    <asp:ListItem>LOST CUSTOMER(Juliana)</asp:ListItem>
                    <asp:ListItem>CONFIRMED(Kirsty)</asp:ListItem>
                    <asp:ListItem>PENDING ACTIVE(Kirsty)</asp:ListItem>
                    <asp:ListItem>LOST PRICE(Kirsty)</asp:ListItem>
                    <asp:ListItem>LOST AVAILABILITY(Kirsty)</asp:ListItem>
                    <asp:ListItem>LOST CUSTOMER(Kirsty)</asp:ListItem>
                    <asp:ListItem>CONFIRMED(Andrew)</asp:ListItem>
                    <asp:ListItem>PENDING ACTIVE(Andrew)</asp:ListItem>
                    <asp:ListItem>LOST PRICE(Andrew)</asp:ListItem>
                    <asp:ListItem>LOST AVAILABILITY(Andrew)</asp:ListItem>
                    <asp:ListItem>LOST CUSTOMER(Andrew)</asp:ListItem>
					 <asp:ListItem>DUPLICATE</asp:ListItem>
                    </asp:DropDownList></td>
                <asp:RequiredFieldValidator ControlToValidate="drpStatus" ID="RFVCOnfirm"
ValidationGroup="Confirm" ErrorMessage="Please select a Status"
InitialValue="0" runat="server" ForeColor="Red"  Display="Dynamic">
</asp:RequiredFieldValidator>
                
                <td width="4%"><asp:Button runat="server" ID="btnConfirmQuote" Text="Confirm Quote" Visible ="false" ValidationGroup="Confirm"   Width ="120px" OnClick="btnConfirmQuote_Click"/></td>
                <td> <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                <td width="4%"><asp:Button runat="server" Text="Submit Quote" ID ="btnSubmit" Visible="false" ValidationGroup="Submit"  OnClick="btnSubmit_Click"/> </td>
                <td> <asp:Button runat="server" Text="Save Quote" ID ="btnSave" ValidationGroup="Save" Visible="false" OnClick="btnSave_Click" />  </td>
                <td> <asp:Button runat="server" Text="Delete Quote" ID ="btnDelete" Visible="false" OnClick="btnDelete_Click" />  </td>
                
                
            </tr>
            <tr>
                <td colspan="6"></td>
                 <td ><asp:Label ID="Label1" runat="server" Text="Update Status Comment" ></asp:Label>
                <asp:TextBox ID="txtUpdateStatusCmt" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            </table>
    </form>
</body>
</html>
