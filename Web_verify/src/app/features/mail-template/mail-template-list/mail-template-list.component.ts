import { Component, Injector, ViewChild } from "@angular/core";
import { PageEvent } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import {
    ComponentAbstract,
    DformPaginationPlusComponent
} from "@shared-sm";
import { finalize, takeUntil } from "rxjs";
import { CreateMailTemplateComponent } from "../create-mail-template/create-mail-template.component";
import { subject, templateId } from "../data-form/search-mail-template";
import { MailTemplateService } from "../services/mail-template.service";
import { UpdateMailTemplateComponent } from "../update-mail-template/update-mail-template.component";

@Component({
	selector: "app-mail-template-list",
	templateUrl: "./mail-template-list.component.html",
	styleUrls: ["./mail-template-list.component.scss"],
})
export class MailTemplateListComponent extends ComponentAbstract {
    $templateId = templateId();
    $subject = subject();
    openPanelSearchAdvanced = false;
    @ViewChild('inputElement') inputElement: any;

	displayedColumns = ["stt", "id", "subject", "description", "createdDate", "action"];
    dataTable = [];
	public tableDataSource!: MatTableDataSource<any>;

	@ViewChild("pagePage", {
		static: true,
	})
	dformPaging!: DformPaginationPlusComponent;
	pageEvent: PageEvent = new PageEvent();
	public pageSize = 10;
	public pageIndex = 0;
	public totalItem = 0;

	constructor(
		protected injector: Injector,
        private mailTemplateService: MailTemplateService,
	) {
		super(injector);
	}

    componentInit(): void {
        this.form = this.itemControl.toFormGroup([
            this.$templateId,
            this.$subject
        ]);
        this.getList();
	}

    getList() {
        this.indicator.showActivityIndicator();
        try {
            let templateId = this.form.value.templateId ? this.form.value.templateId : "";
            let subject = this.form.value.subject ? this.form.value.subject : "";
            if(!this.openPanelSearchAdvanced) {
                templateId = "";
                subject = this.inputElement?.nativeElement?.value ? this.inputElement.nativeElement.value : "";
            }

            this.mailTemplateService.getListMailTemplate(this.pageIndex, this.pageSize, templateId, subject)
                .pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    if(res) {
                        this.totalItem = res.content.totalElements;
                        const page = this.pageIndex * this.pageSize;
                        this.dataTable = res.content.content.map((x, index) => {
                            return {...x, stt: page + index + 1};
                        });
                        this.tableDataSource = new MatTableDataSource(this.dataTable);
                    }
                })
        } catch (error) {
            console.log(error);
            this.indicator.hideActivityIndicator();
        }
    }

    handlePageEvent(e: PageEvent) {
        this.pageEvent = e;
        this.pageSize = e.pageSize;
        this.pageIndex = e.pageIndex;
        this.getList()
    }

    loadDataTable() {
        this.totalItem = this.dataTable.length;
        this.pageSize = 5;
        this.tableDataSource = new MatTableDataSource(this.dataTable);
    }

    createMailTemplate() {
        this.dialogService.componentDialog(CreateMailTemplateComponent, {
            width: '60%',
            data: {}
        }, (data) => {
            if (data && data.id) {
                this.indicator.showActivityIndicator();
                this.mailTemplateService.createMailTemplate(data).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    console.log(res);
                    if(res) {
                        this.dataTable.push({stt: this.dataTable.length + 1, ...res.content});
                        this.loadDataTable();
                    }
                });
            }
        });
    }

    updateMailTemplate(element) {
        this.dialogService.componentDialog(UpdateMailTemplateComponent, {
            width: '60%',
            data: { element }
        }, (data) => {
            if (data && data.id) {
                this.indicator.showActivityIndicator();
                this.mailTemplateService.updateMailTemplate(data).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    this.getList();
                });
            }
        });
    }

    deleteMailTemplate(id) {
        if(!id) return;

        this.dialogService.confirm(
			{
				title: "Xác nhận",
				message: "Bạn chắc chắn muốn xóa template này?",
			},
			(result) => {
				if (result) {
					this.mailTemplateService
						.deleteMailTemplate(id)
						.pipe(takeUntil(this.ngUnsubscribe))
						.subscribe((res) => {
                            this.getList();
						});
				}
			}
		);
    }
}
