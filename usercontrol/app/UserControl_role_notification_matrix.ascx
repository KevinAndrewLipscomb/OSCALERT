<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UserControl_role_notification_matrix.ascx.cs" Inherits="UserControl_role_notification_matrix.TWebUserControl_role_notification_matrix"%>
<table bordercolor="#dcdcdc" cellspacing="0" cellpadding="0" border="1">
  <tr>
    <td>
      <table cellspacing="0" cellpadding="10" border="0">
        <tr>
          <td bgcolor="whitesmoke">
            <p><small><em>Please wait for system to refresh after each change.</em></small></p>
          </td>
        </tr>
        <tr>
          <td>
            <asp:GridView id="GridView_control" runat="server" gridlines="Horizontal" cellpadding="10" allowsorting="True" emptydatatext="No data available" bordercolor="Gainsboro" borderstyle="Solid">
              <HeaderStyle backcolor="WhiteSmoke" font-size="X-Small"></HeaderStyle>
            </asp:GridView>
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>
