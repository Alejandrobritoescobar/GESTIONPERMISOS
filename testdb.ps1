$conn = New-Object -ComObject "ADODB.Connection"
$connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=d:\Release\Sistemapermisos.accdb;"
try {
    $conn.Open($connStr)
    if ($conn.State -eq 1) { 
        Write-Output "BD_CONEXION_OK"
    } else {
        Write-Output "BD_ERROR_ESTADO"
    }
    $conn.Close()
} catch {
    Write-Output "BD_EXCEPTION: $($_.Exception.Message)"
}
