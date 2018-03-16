
# connection
$sourceDbPickup = "DbPickup"

# input
$root = "C:\output\oracles\";

# read source database structure 
$datas = $root + $sourceDbPickup + ".config"
$output = Open-OracleModel -InputFilename $datas