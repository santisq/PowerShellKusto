$moduleName = [System.IO.Path]::GetFileNameWithoutExtension($PSCommandPath)
$context = [System.IO.Path]::Combine($PSScriptRoot, 'bin', 'net6', "$moduleName.Shared.dll")
$isReload = $true

if (-not ('PowerShellKusto.Shared.LoadContext' -as [type])) {
    $isReload = $false
    Add-Type -Path $context
}

$mainModule = [PowerShellKusto.Shared.LoadContext]::Initialize()
$innerMod = Import-Module -Assembly $mainModule -PassThru:$isReload

if ($innerMod) {
    $addExportedCmdlet = [System.Management.Automation.PSModuleInfo].GetMethod(
        'AddExportedCmdlet',
        [System.Reflection.BindingFlags] 'Instance, NonPublic')

    foreach ($cmd in $innerMod.ExportedCmdlets.Values) {
        $addExportedCmdlet.Invoke($ExecutionContext.SessionState.Module, @($cmd))
    }
}
