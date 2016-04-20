Param(  [parameter(Mandatory=$true)][string[]]$Tables,
        [parameter(Mandatory=$true)][string]$ServerName,
        [parameter(Mandatory=$true)][string]$DatabaseName,
        [string]$UserName,
        [string]$Password,
        [Switch]$IncludeIdentity
)

function DbTypeFor($name)
{
    "SqlDbType." + [System.Data.SqlDbType]::$name
}

function AllowNulls($value)
{
    if ($value -eq 0){ "false" } else { "true" }
}

function SizeAllowed($dataType)
{
    switch ($dataType.ToLower())
    {
        'binary' {$true}
        'char' {$true}
        'decimal' {$true}
        'nchar' {$true}
        'nvarchar' {$true}
        'varbinary' {$true}
        'varchar' {$true}
        default {$false}
    }
}

Push-Location

Import-Module SQLPS

$Tables | % {

    $tableName = $_

    #http://stackoverflow.com/questions/2418527/sql-server-query-to-get-the-list-of-columns-in-a-table-along-with-data-types-no
    $query = @" 
        SELECT 
        c.name 'Column Name',
        t.Name 'Data type',
        c.scale,
        c.is_nullable, 
        ISNULL(i.is_primary_key, 0) 'Primary Key'

        FROM    
            sys.columns c
        INNER JOIN 
            sys.types t ON c.user_type_id = t.user_type_id
        LEFT OUTER JOIN 
            sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        LEFT OUTER JOIN 
            sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
        WHERE
            c.object_id = OBJECT_ID('$tableName')
"@

    $columns = Invoke-Sqlcmd -Query $query -ServerInstance $ServerName -Database $DatabaseName -Username $UserName -Password $Password

    $columnDefinitions = @()

    $columns | % {

        $definition = "new ColumnDefinition { Name = `"$($_.'Column Name')`", DataType = $(DbTypeFor($_.'Data type')), AllowNulls = $(AllowNulls($_.is_nullable))"

        if (SizeAllowed($_.'Data type'))
        {
            #this is temporary, until all the sql above gets replaced with INFORMATION_SCHEMA query
            $query = "SELECT CHARACTER_MAXIMUM_LENGTH 'Size' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '$tableName' AND COLUMN_NAME = '$($_.'Column Name')'"
            $columns = Invoke-Sqlcmd -Query $query -ServerInstance $ServerName -Database $DatabaseName -Username $UserName -Password $Password

            $definition += ", Size = $($columns.Size)"

        }

        if ($_.'Data type'.ToUpper() -eq "DECIMAL")
        {
            $definition += ", DecimalPlaces = $($_.scale)"
        }

        if ($IncludeIdentity -and ($_.'Primary Key' -eq 1))
        {
            $definition += ", IdentitySeed = 1"
        }
    
        $definition += " },"

        $columnDefinitions += $definition
    }

    $columnDefinitions | clip
    
    Write-Host "Column definitions for $_ : `n"
    $columnDefinitions
    Write-Host "`n`n"
}

Pop-Location
