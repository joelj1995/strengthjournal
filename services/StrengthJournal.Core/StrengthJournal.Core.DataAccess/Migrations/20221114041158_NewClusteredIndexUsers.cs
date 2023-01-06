using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthJournal.DataAccess.Migrations
{
    public partial class NewClusteredIndexUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
ALTER TABLE [dbo].[WorkoutLogEntries] DROP CONSTRAINT [FK_WorkoutLogEntries_Users_UserId]
GO

ALTER TABLE [dbo].[Exercises] DROP CONSTRAINT [FK_Exercises_Users_CreatedByUserId]
GO

ALTER TABLE dbo.Users
DROP CONSTRAINT [PK_Users]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

DROP INDEX [IX_Users_ExternalId] ON [dbo].[Users]

CREATE CLUSTERED INDEX CX_Users ON dbo.Users (ExternalId);

ALTER TABLE [dbo].[Exercises]  WITH CHECK ADD  CONSTRAINT [FK_Exercises_Users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Exercises] CHECK CONSTRAINT [FK_Exercises_Users_CreatedByUserId]
GO

ALTER TABLE [dbo].[WorkoutLogEntries]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutLogEntries_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[WorkoutLogEntries] CHECK CONSTRAINT [FK_WorkoutLogEntries_Users_UserId]
GO
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
