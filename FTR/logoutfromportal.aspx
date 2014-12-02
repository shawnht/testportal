<%@ Page Language="C#" CodeBehind="logoutfromportal.aspx.cs" Inherits="GleamTech.FileVista.Web.DefaultPage" %>

<!DOCTYPE html>

<%=ApplicationInfo%>
<html>
    <head runat="server">
        <title></title>
        <script type="text/javascript">
            function logout() {
             GleamTech.JavaScript.Util.RequestJson(getActionUrl("Logout"), null, function (response) { parent.location.href = "http://portal.ag-risk.org/"; }, function (response) { alert(fileManager.Language.GetString("544") + "\n\n" + response); });
            }
        </script>
    </head>
    <body>   
        <script type="text/javascript">
            fileVistaParameters = {
                ResourceBasePath: "<%=ResourceBasePath%>",
                ActionBasePath: "<%=ActionBasePath%>",
                IsAdministrator: <%=IsAdministrator%>,
                IsGroupManager: <%=IsGroupManager%>,
                CanLogout: <%=CanLogout%>
            };
         logout(); 
       </script>
        <GleamTech:FileManager ID="fileManager" runat="server" />
    </body>
</html>
