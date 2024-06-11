using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a4fa764-01fd-4a07-bfae-b65785c24cd6", "AQAAAAIAAYagAAAAEGj/8dasesjumANMPoIugezs0J2RD6dlbbOhkdRnmqv9F/vEMXpuQR82UP+GEGGjDg==", "015c36d2-181e-4d4b-acbb-72ea86143535" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a41b8359-e559-4641-b83e-0886dad4ad26", "AQAAAAIAAYagAAAAEE4IgvvZ7AkZrGsU/bGU2HWgvCJ1xQUDj5RjqE6udtIYmsvEzDq3j+GqytQI5UaEnQ==", "a3739e00-dd7b-4262-a340-0b500d7931c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9d9bd92-c116-42cb-b961-06983157ee05", "AQAAAAIAAYagAAAAEODaG8SfTzihiwCJIrQ1sJdKen0KCro2eHitpXZtXBLzXRr/JnAPjwzMBwa4B9Crrw==", "6898ed90-99c9-43c7-b121-2b91568468ae" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 6, 12, 0, 18, 48, 122, DateTimeKind.Local).AddTicks(4017));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 6, 12, 0, 18, 48, 122, DateTimeKind.Local).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 6, 12, 0, 18, 48, 122, DateTimeKind.Local).AddTicks(4022));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 6, 12, 0, 18, 48, 122, DateTimeKind.Local).AddTicks(3856));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 6, 12, 0, 18, 48, 122, DateTimeKind.Local).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 6, 12, 0, 18, 48, 122, DateTimeKind.Local).AddTicks(3872));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab595b81-d707-4963-8578-3d8797e4845e", "AQAAAAIAAYagAAAAEFofNR73+nPpMlaD3kme92qDpt9XKhyC94kHhmnoVgZDbVk25RogxHRazyyfAs2NxQ==", "fb2cf86a-0981-48b2-84be-b9016f8d4020" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fea5b47a-f189-44ef-9998-8559e596c586", "AQAAAAIAAYagAAAAEIVJnTqC8p0M6q7G4InMPtL3lzYLQfHVMzGXNWCesQzE5Rh7O320PMeuMGLICX3Dkg==", "88c8c424-4d01-4fc3-8e0d-6e6a6ce58f82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2ae943b-5d65-42c2-a251-8efd7100babf", "AQAAAAIAAYagAAAAEHsv16Sg1+HvLiJmUf8az0QGyOeH7wr6rUVgEJSFPoTUAhTuSzCbfNG/QZ9V4HFC9Q==", "9ee1aa6e-fecc-4f66-bb05-d9beb83f5c41" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 6, 5, 17, 11, 31, 44, DateTimeKind.Local).AddTicks(6312));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 6, 5, 17, 11, 31, 44, DateTimeKind.Local).AddTicks(6315));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 6, 5, 17, 11, 31, 44, DateTimeKind.Local).AddTicks(6317));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 6, 5, 17, 11, 31, 44, DateTimeKind.Local).AddTicks(5832));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 6, 5, 17, 11, 31, 44, DateTimeKind.Local).AddTicks(5851));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 6, 5, 17, 11, 31, 44, DateTimeKind.Local).AddTicks(5853));
        }
    }
}
