
<html> 
  <head> 
  <script type="text/javascript">
      var query = window.location.search.substring(1);
      if (query == "") {
          //alert("loginfromportal.htm: auto login from Portal failed. No valid login and password.");
          window.location = "http://portal.ag-risk.org/portal/portalerror.htm?error="+"no username and password!";
      }
      else {
          //alert("loginfromportal.htm: going to login.aspx!");
          //alert(query);
          window.location = "http://portal.ag-risk.org/ftr/login.aspx?" + query;
      }
  </script>
  </head> 
</html>
