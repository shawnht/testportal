<%@ Page Language="C#" CodeBehind="Login.aspx.cs" Inherits="GleamTech.FileVista.Web.LoginPage" %>

<!DOCTYPE html>

<html>
    <head runat="server">
		<title></title>          
	</head>
	<body onload="autologin()">
        <form id="formLogin" runat="server" method="post" onsubmit="return login();">
            <input type="hidden" id="loginAction" value="<%=LoginAction%>"/>
            <div id="divLogin">
                <div class="gt-panel" style="padding: 5px">
				    <table  border="0" cellpadding="0" cellspacing="5" style="width: 280px; display:none">
                        <tr>
                            <td colspan="2">
			                    <table border="0" cellpadding="5" cellspacing="0" style="width: 280px;">
				                    <tr>
					                    <td style="text-align: center"><img style="border: none; width: 48px; height: 48px" src="<%=LoginImageUrl%>" alt="" /></td>
					                    <td><%=Language.GetEntry("500", Title)%><br /><%=Language.GetEntry("501")%></td>
				                    </tr>
				                </table>                        
                            </td>
                        </tr>
	                    <tr>
	                        <td colspan="2" style="padding-bottom: 5px"><div class="gt-panel" style="height: 0px"></div></td>
	                    </tr>
                        <tr>
					        <td style="width: 120px;"><%=Language.GetEntry("502")%></td>
					        <td><input name="username" type="text" value="<%=HttpUtility.ParseQueryString(this.ClientQueryString).Get("username") %>" style="width: 170px" /></td>
				        </tr>
				        <tr>
					        <td><%=Language.GetEntry("503")%></td>
					        <td><input name="password" type="password" value="<%=HttpUtility.ParseQueryString(this.ClientQueryString).Get("password") %>" style="width: 170px" /></td>
				        </tr>
				        <tr>
					        <td><%=Language.GetEntry("664")%>:</td>
					        <td><asp:DropDownList ID="DropDownListLanguages" runat="server" style="width: 175px"></asp:DropDownList></td>
				        </tr>
				        <tr>
                            <td></td>
                            <td><input id="remember" name="remember" type="checkbox" /><label for="remember"><%=Language.GetEntry("652")%></label></td>
				        </tr>
				        <tr>
					        <td colspan="2" style="text-align: right">
						        <input name="submitButton" type="submit" value="<%=Language.GetEntry("504")%>" style="width: 100px;" />
					        </td>
				        </tr>
			        </table>
                </div>
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <br />
		        <%=DemoInfo%>
            </div>		
        </form>
        <script type="text/javascript">
            function autologin() {
                var query = window.location.search.substring(1);
                //alert("login.aspx: "+query);
                if (query == "") {
                //if (1==2) {
                    //alert("login.aspx: auto login from Portal failed. No valid login and password.");
                    window.location = "http://portal.ag-risk.org/portal/portalerror.htm?error=" + "FTR direct login is disabled. Must automatically login from Portal!";
                }
                else {
                    //alert("login.aspx: going to login.aspx!");
                    login();
                }
            }
        </script>
	</body>
</html>
