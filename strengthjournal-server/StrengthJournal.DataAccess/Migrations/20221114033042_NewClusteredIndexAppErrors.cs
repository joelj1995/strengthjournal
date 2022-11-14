using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class NewClusteredIndexAppErrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
ALTER TABLE dbo.AppErrors
DROP CONSTRAINT [PK_AppErrors]
GO

ALTER TABLE [dbo].[AppErrors] ADD  CONSTRAINT [PK_AppErrors] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE CLUSTERED INDEX CX_AppErrors ON dbo.AppErrors (DateCreated);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP INDEX CX_AppErrors ON dbo.AppErrors;

ALTER TABLE dbo.AppErrors
DROP CONSTRAINT [PK_AppErrors]
GO

ALTER TABLE [dbo].[AppErrors] ADD  CONSTRAINT [PK_AppErrors] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
");
        }
    }
}
