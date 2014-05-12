<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UserControl_establish_membership.ascx.cs" Inherits="UserControl_establish_membership.TWebUserControl_establish_membership"%>
<table bordercolor="#dcdcdc" cellspacing="0" cellpadding="0" border="1">
  <tr>
    <td>
      <table cellspacing="0" cellpadding="10" width="100%" border="0">
        <tr bgcolor="#f5f5f5">
          <td><strong>Establish membership</strong>
          </td>
        </tr>
        <tr>
          <td>
                <p>Your <ASP:Label id="Label_application_name_1" runat="server"></ASP:Label>&nbsp;username is not currently linked to a&nbsp;<ASP:Label id="Label_sponsor_1" runat="server"></ASP:Label> membership record.</p>
                <p>Are you already a <ASP:Label id="Label_sponsor_2" runat="server"></ASP:Label>&nbsp;member?</p>
                <p>
                  <table cellspacing="0" cellpadding="10" border="0">
                      <tr>
						<td align="center" valign="top"><strong>NO</strong></td>
						<td valign="top" nowrap="nowrap">-&gt;</td>
                        <td valign="top">
                     Use the process dictated by the <asp:Label id="Label_sponsor_4" runat="server"></asp:Label> to attain a <asp:Label id="Label_shared_secret_description_2" runat="server"></asp:Label>.
                          <p>&nbsp;</p></td>
                      </tr>
                      <tr>
						<td align="center" valign="top"><strong>YES</strong></td>
						<td valign="top" nowrap="nowrap">-&gt;</td>
						<td valign="top">
						  Match yourself to a <ASP:Label id="Label_sponsor_3" runat="server"></ASP:Label>&nbsp;membership record by entering <ASP:Label id="Label_shared_secret_description_1" runat="server"></ASP:Label>&nbsp;here:
						  <p>
							  <asp:textbox id="TextBox_noop_ie_behavior_workaround" runat="server" style="DISPLAY: none; VISIBILITY: hidden" /><!-- To work around an IE bug that otherwise prevents Enter in a textbox from submitting a form. -->
							  <ASP:TextBox id="TextBox_shared_secret" runat="server" columns="15" maxlength="15"></ASP:TextBox>
                <ASP:Button id="Button_submit" runat="server" text="Submit" font-bold="True" onclick="Button_submit_Click"></ASP:Button>
                <ASP:RequiredFieldValidator id="RequiredFieldValidator_shared_secret" runat="server" errormessage="Please enter {your shared secret data}." font-bold="True" controltovalidate="TextBox_shared_secret">!ERR!</ASP:RequiredFieldValidator>
                <ASP:RegularExpressionValidator id="RegularExpressionValidator_shared_secret" runat="server" errormessage="Please enter only numbers." font-bold="True" controltovalidate="TextBox_shared_secret" validationexpression="\d+">!ERR!</ASP:RegularExpressionValidator>
                <asp:CustomValidator ID="CustomValidator_shared_secret" runat="server" Display="Dynamic" ErrorMessage="Sorry, because of privileges attached to the specified membership record, Application Administrator intervention is required to complete this match.  The Application Administrator has been notified and will be in touch with you." Font-Bold="True" OnServerValidate="CustomValidator_shared_secret_ServerValidate">!ERR!</asp:CustomValidator>
						  </p>
						  <p>If you have trouble with this feature, <ASP:LinkButton id="LinkButton_trouble_handler" runat="server" causesvalidation="False" onclick="LinkButton_trouble_handler_Click">click here</ASP:LinkButton>.</p>
						  <p>
							<table id="Table_proceed" runat="server" visible="False" bordercolor="#0000ff" cellspacing="0" cellpadding="10" border="3">
							  <tr>
								<td><ASP:LinkButton id="LinkButton_proceed" runat="server" font-bold="True" onclick="LinkButton_proceed_Click">CLICK HERE TO CONTINUE</ASP:LinkButton></td>
							  </tr>
							</table>
						  </p>
						</td>
					  </tr>
				  </table>
                </p>
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
