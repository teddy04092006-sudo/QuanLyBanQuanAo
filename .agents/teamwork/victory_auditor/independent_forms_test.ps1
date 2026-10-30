[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
Add-Type -AssemblyName System.Windows.Forms

$projectRoot = Split-Path (Split-Path (Split-Path $PSScriptRoot -Parent) -Parent) -Parent
$exePath = Join-Path $projectRoot "bin\Debug\QuanLyBanQuanAo.exe"

$asm = [System.Reflection.Assembly]::LoadFrom($exePath)

$formsToCheck = @(
    @{ FormName = "QuanLyBanQuanAo.GUI.frmQuanAo"; PicField = "picAnh"; ExpectedMode = "Zoom" },
    @{ FormName = "QuanLyBanQuanAo.GUI.frmCuaHang"; PicField = "picCT"; ExpectedMode = "Zoom" },
    @{ FormName = "QuanLyBanQuanAo.GUI.frmDatHang"; PicField = "picXemTruoc"; ExpectedMode = "Zoom" }
)

Write-Output "=== Checking PictureBox SizeMode on Instantiated Forms ==="

$allPassed = $true

foreach ($item in $formsToCheck) {
    $t = $asm.GetType($item.FormName)
    if ($null -eq $t) {
        Write-Error "Form type not found: $($item.FormName)"
        $allPassed = $false
        continue
    }

    $formInstance = [System.Activator]::CreateInstance($t)
    $field = $t.GetField($item.PicField, [System.Reflection.BindingFlags]"NonPublic,Instance,Public")
    if ($null -eq $field) {
        Write-Error "Field $($item.PicField) not found on $($item.FormName)"
        $allPassed = $false
    } else {
        $picBox = $field.GetValue($formInstance)
        $actualMode = $picBox.SizeMode.ToString()
        Write-Output "$($item.FormName).$($item.PicField).SizeMode = $actualMode (Expected: $($item.ExpectedMode))"
        if ($actualMode -ne $item.ExpectedMode) {
            $allPassed = $false
        }
    }
    $formInstance.Dispose()
}

if ($allPassed) {
    Write-Output "VERDICT: ALL PICTUREBOX SIZE MODES VERIFIED ZOOM (PASS)"
    exit 0
} else {
    Write-Output "VERDICT: SIZE MODE MISMATCH (FAIL)"
    exit 1
}
