using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fit_Elite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Users_GymOwnerId",
                table: "Gyms");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_Gyms_GymId",
                table: "MemberSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_SubscriptionPlans_SubscriptionPlanId",
                table: "MemberSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_Users_MemberId",
                table: "MemberSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_MemberSubscriptions_MemberSubscriptionId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Gyms_GymId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Users_GymOwnerId",
                table: "Gyms",
                column: "GymOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_Gyms_GymId",
                table: "MemberSubscriptions",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_SubscriptionPlans_SubscriptionPlanId",
                table: "MemberSubscriptions",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_Users_MemberId",
                table: "MemberSubscriptions",
                column: "MemberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_MemberSubscriptions_MemberSubscriptionId",
                table: "Payments",
                column: "MemberSubscriptionId",
                principalTable: "MemberSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Gyms_GymId",
                table: "SubscriptionPlans",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Users_GymOwnerId",
                table: "Gyms");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_Gyms_GymId",
                table: "MemberSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_SubscriptionPlans_SubscriptionPlanId",
                table: "MemberSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscriptions_Users_MemberId",
                table: "MemberSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_MemberSubscriptions_MemberSubscriptionId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Gyms_GymId",
                table: "SubscriptionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Users_GymOwnerId",
                table: "Gyms",
                column: "GymOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_Gyms_GymId",
                table: "MemberSubscriptions",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_SubscriptionPlans_SubscriptionPlanId",
                table: "MemberSubscriptions",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscriptions_Users_MemberId",
                table: "MemberSubscriptions",
                column: "MemberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_MemberSubscriptions_MemberSubscriptionId",
                table: "Payments",
                column: "MemberSubscriptionId",
                principalTable: "MemberSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Gyms_GymId",
                table: "SubscriptionPlans",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
