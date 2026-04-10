$conn = New-Object -ComObject ADODB.Connection
$conn.Open("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=d:\Release\Sistemapermisos.accdb;")
$rs = $conn.Execute("SELECT TOP 1 * FROM Permisos")
for ($i = 0; $i -lt $rs.Fields.Count; $i++) {
    Write-Output ($rs.Fields.Item($i).Name + " -> " + $rs.Fields.Item($i).Type)
}
$conn.Close()
