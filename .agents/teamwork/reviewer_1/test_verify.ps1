Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

$projRoot = (Get-Item $PSScriptRoot).Parent.Parent.Parent.FullName
$binDir = Join-Path $projRoot 'bin\Debug\Images\QuanAo'
$srcDir = Join-Path $projRoot 'Images\QuanAo'
$exePath = Join-Path $projRoot 'bin\Debug\QuanLyBanQuanAo.exe'

Write-Host "Project Root: $projRoot"

# 1. Check file counts and dimensions
$binQA = Get-ChildItem -LiteralPath $binDir -Filter 'QA*.jpg'
$srcQA = Get-ChildItem -LiteralPath $srcDir -Filter 'QA*.jpg'

Write-Host "Bin QA count: $($binQA.Count)"
Write-Host "Src QA count: $($srcQA.Count)"

$dimErrors = 0
$corruptCount = 0
$minSize = 999999999
$maxSize = 0

for ($i = 1; $i -le 200; $i++) {
    $name = ('QA{0:D3}.jpg' -f $i)
    $bp = Join-Path $binDir $name
    $sp = Join-Path $srcDir $name
    if (-not (Test-Path -LiteralPath $bp) -or -not (Test-Path -LiteralPath $sp)) {
        Write-Host "MISSING: $name"
        $corruptCount++
        continue
    }
    
    $fi = Get-Item -LiteralPath $bp
    if ($fi.Length -lt $minSize) { $minSize = $fi.Length }
    if ($fi.Length -gt $maxSize) { $maxSize = $fi.Length }
    
    try {
        $img = [System.Drawing.Image]::FromFile($bp)
        if ($img.Width -ne 500 -or $img.Height -ne 600) {
            Write-Host "DIM MISMATCH in $name : $($img.Width)x$($img.Height)"
            $dimErrors++
        }
        $img.Dispose()
    } catch {
        Write-Host "FAILED TO READ IMAGE $name : $($_.Exception.Message)"
        $corruptCount++
    }
}

Write-Host "Min file size: $minSize bytes | Max file size: $maxSize bytes"
Write-Host "Corrupt / Missing count: $corruptCount"
Write-Host "Dimension errors: $dimErrors"

# 2. Test Reflection & LayAnhKhongKhoaFile
$asm = [System.Reflection.Assembly]::LoadFrom($exePath)
$funcType = $asm.GetType('QuanLyBanQuanAo.BLL.Function')
$methodLayAnh = $funcType.GetMethod('LayAnhKhongKhoaFile', [System.Reflection.BindingFlags]'Public,Static')

# 2a. Edge cases: null, empty, non-existent
$imgNull = $methodLayAnh.Invoke($null, @([string]$null))
$imgEmpty = $methodLayAnh.Invoke($null, @(""))
$imgNonExist = $methodLayAnh.Invoke($null, @("C:\non_existent_path_xyz123.jpg"))

Write-Host "Edge case test (null -> $(if ($imgNull -eq $null) {'NULL'} else {'NON-NULL'}), empty -> $(if ($imgEmpty -eq $null) {'NULL'} else {'NON-NULL'}), nonExist -> $(if ($imgNonExist -eq $null) {'NULL'} else {'NON-NULL'}))"

# 2b. Concurrency & file lock test
$sampleFile = [string](Join-Path $binDir 'QA001.jpg')
$imgSample = $methodLayAnh.Invoke($null, [object[]]@($sampleFile))
$lockFreed = $false
try {
    # Attempt exclusive write access while image is held in memory
    $fs = [System.IO.File]::Open($sampleFile, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
    $lockFreed = $true
    $fs.Close()
} catch {
    $lockFreed = $false
    Write-Host "LOCK FAILED: $($_.Exception.Message)"
}
if ($imgSample -ne $null) { $imgSample.Dispose() }
Write-Host "Lock freed test: $lockFreed"

# 3. Test PictureBox SizeMode across GUI forms
$frmQA = New-Object QuanLyBanQuanAo.GUI.frmQuanAo
$frmCH = New-Object QuanLyBanQuanAo.GUI.frmCuaHang
$frmDH = New-Object QuanLyBanQuanAo.GUI.frmDatHang

$smQA = $frmQA.Controls.Find('picAnh', $true)[0].SizeMode
$smCH = $frmCH.Controls.Find('picCT', $true)[0].SizeMode
$smDH = $frmDH.Controls.Find('picXemTruoc', $true)[0].SizeMode

Write-Host "SizeMode frmQuanAo.picAnh: $smQA"
Write-Host "SizeMode frmCuaHang.picCT: $smCH"
Write-Host "SizeMode frmDatHang.picXemTruoc: $smDH"

$frmQA.Dispose()
$frmCH.Dispose()
$frmDH.Dispose()

# 4. Check Log file
$logPath = Join-Path $projRoot 'Logs\image_gen.log'
if (Test-Path -LiteralPath $logPath) {
    $logContent = Get-Content -LiteralPath $logPath -Tail 5
    Write-Host "Latest image_gen.log entries:"
    $logContent | ForEach-Object { Write-Host "  $_" }
} else {
    Write-Host "Log file missing!"
}

# 5. Test TaoTuDong200AnhSanPham idempotence execution
$fInstance = [Activator]::CreateInstance($funcType)
$methodTaoTuDong = $funcType.GetMethod('TaoTuDong200AnhSanPham')
Write-Host "Invoking TaoTuDong200AnhSanPham()..."
$methodTaoTuDong.Invoke($fInstance, $null)
Write-Host "TaoTuDong200AnhSanPham() executed successfully without error!"

