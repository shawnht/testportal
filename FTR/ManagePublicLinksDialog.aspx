<%@ Page Language="C#" CodeBehind="ManagePublicLinksDialog.aspx.cs" Inherits="GleamTech.FileVista.Web.ManagePublicLinksDialogPage" %>

<!DOCTYPE html>

<%=ApplicationInfo%>
<html>
    <head runat="server">
        <title><%=Language.GetEntry("FileVista.Label.ManagePublicLinks")%></title>
    </head>
    <body>
        <script type="text/javascript"<%=Request.Browser.Browser == "IE"
                            && Request.Browser.MajorVersion <= 8 ? " defer" : "" %>>
            var managePublicLinksDialog = new GleamTech.FileVista.ManagePublicLinksDialog({
                ElementId: "managePublicLinksDialog",
                Language: "<%=Language.Name%>",
                ResourceBasePath: "<%=ResourceBasePath%>",
                FileUltimateResourceBasePath: "<%=FileUltimateResourceBasePath%>",
                ActionBasePath: "<%=ActionBasePath%>",
                RootFolderId: <%=RootFolderId%>,
                Path: "<%=Path%>",
                FileName: "<%=FileName%>",
                PublicLinks: <%=PublicLinks%>
            });
        </script>
    </body>
</html>
