<?php
session_start();
?>
<!DOCTYPE html>
<html>
<head>

<title>Poker Homey</title>
<link href="https://mottie.github.io/tablesorter/css/theme.default.css" rel="stylesheet">
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script> 
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.tablesorter/2.9.1/jquery.tablesorter.min.js"></script>

<script>
        $(function(){
		  $("#table1").tablesorter({sortList: [[5,1]], widgets: ['zebra']});
		  $("#table2").tablesorter({sortList: [[4,1]], widgets: ['zebra']});
          $("#table3").tablesorter({sortList: [[1,1]], widgets: ['zebra']});
        });
    </script>
<style>
body{
	background-image: url("images/homeBack4.jpg");
	background-size: 600px 800px;
	color : white;
}

table {
	<--! background-image : url("images/MainMenuBack.jpg"); -->
}

table ,th,td{
	border: 2px solid yellow;
    border-collapse: collapse;
}

button {
    background-color: #ff4d4d;
	width : 100px;
	height : 20px;
	text-align: center;
    color: white;
    margin: 8px 0;
    border: none;
    cursor: pointer;
	float : right;
}

button:hover {
    opacity: 0.8;
}



</style

</head>

<body>
	<button onClick = logoutFunc() type="button">Logout</button>
	<script>
		function logoutFunc(){
			window.location="index.php";
		}
	</script>

<?php
	if(!$_SESSION['username']){
		header("location: index.php");
		exit();
	}	
	$result = $_SESSION['info']; //in[0] myPlayer info , in[1] top by Gross profit, in[2] top by Highest gain , in[3] top by Number of games

	if (($result))
	{	
		echo "<h1 align='center'> Main Statistics </h1>";
		
		echo "<table width='100%'>";
		echo "<tr>";
		$columns = array("Username","Number of games","Best win","Total gross","average gain","Average gross profit");
		
		$tablesName= array("Player Info","Top by Gross Profit","Top by Highest Gain","Top By Number Of Games");
		$tableSize=count($tablesName);
		for ($i=0 ; $i <$tableSize; $i++){
			echo "<h2> $tablesName[$i] </br> </h2>";
			echo "<table id=\"table$i\" ellspacing=\"1\" class=\"tablesorter\" width='100%'>";
			echo "<thead>";
			
			foreach ($columns as $col){
				echo "<th> $col </th>";
			}
			
			echo "</thead>";
			echo "<tbody>";
			
			foreach ($result[$i] as $username => $item){
				echo "<tr>";
				echo "<td align='center'> $username </td>";	
				foreach ($item as $it){
					echo "<td align='center'> $it </td>";
				}
				echo "<tr>";
			}
			echo "</tbody>";
			echo "</table>";
		}
	}
session_unset ();



?>



</body>
</html>
