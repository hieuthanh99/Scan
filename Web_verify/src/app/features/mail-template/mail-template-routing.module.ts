import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MailTemplateListComponent } from "./mail-template-list/mail-template-list.component";

const routes: Routes = [
	{
		path: "list",
		component: MailTemplateListComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class MailTemplateRoutingModule {}
