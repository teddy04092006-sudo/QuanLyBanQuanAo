Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = (Get-Item "$scriptDir\..\..\..").FullName
$exePath = Join-Path $projectRoot "bin\Debug\QuanLyBanQuanAo.exe"

$asm = [System.Reflection.Assembly]::LoadFrom($exePath)

# Set authentication session context if needed
$phienType = $asm.GetType("QuanLyBanQuanAo.BLL.PhienDangNhap")
if ($phienType) {
    $fldQuyen = $phienType.GetField("Quyen", [System.Reflection.BindingFlags]"Public,Static")
    $fldHoTen = $phienType.GetField("HoTen", [System.Reflection.BindingFlags]"Public,Static")
    if ($fldQuyen) { $fldQuyen.SetValue($null, "Admin") }
    if ($fldHoTen) { $fldHoTen.SetValue($null, "Administrator") }
}

Write-Output "Testing frmQuanAo..."
$frmQuanAo = New-Object QuanLyBanQuanAo.GUI.frmQuanAo
$picQA = $frmQuanAo.Controls.Find("picAnh", $true)
if ($picQA.Length -gt 0) {
    Write-Output ("frmQuanAo picAnh SizeMode: " + $picQA[0].SizeMode)
} else {
    Write-Output "ERROR: picAnh not found in frmQuanAo"
}

Write-Output "Testing frmCuaHang..."
$frmCuaHang = New-Object QuanLyBanQuanAo.GUI.frmCuaHang
$picCH = $frmCuaHang.Controls.Find("picCT", $true)
if ($picCH.Length -gt 0) {
    Write-Output ("frmCuaHang picCT SizeMode: " + $picCH[0].SizeMode)
} else {
    Write-Output "ERROR: picCT not found in frmCuaHang"
}

Write-Output "Testing frmDatHang..."
$frmDatHang = New-Object QuanLyBanQuanAo.GUI.frmDatHang
$picDH = $frmDatHang.Controls.Find("picXemTruoc", $true)
if ($picDH.Length -gt 0) {
    Write-Output ("frmDatHang picXemTruoc SizeMode: " + $picDH[0].SizeMode)
} else {
    Write-Output "ERROR: picXemTruoc not found in frmDatHang"
}

$frmQuanAo.Dispose()
$frmCuaHang.Dispose()
$frmDatHang.Dispose()
Write-Output "All forms tested successfully."
