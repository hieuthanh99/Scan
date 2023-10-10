import { Component, Injector, ViewChild } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { ComponentAbstract, MessageSeverity, ToastMbService } from "@shared-sm";
import { RedisManagementService } from "../service/redis-management.service";
import { catchError, finalize, map, takeUntil, throwError } from "rxjs";
import { PageEvent } from "@angular/material/paginator";

@Component({
	selector: "app-redis-list",
	templateUrl: "./redis-list.component.html",
	styleUrls: ["./redis-list.component.scss"],
})
export class RedisListComponent extends ComponentAbstract {
	displayedColumns = ["stt", "key", "expireTime", "action"];
	@ViewChild('inputRedisKeyElement') inputRedisKeyElement: any;

	constructor(
		protected injector: Injector,
		private redisService: RedisManagementService
	) {
		super(injector);
	}

	componentInit(): void {
		this.getRedisList();
	}

	getRedisList() {
		this.indicator.showActivityIndicator();

		let findKey = this.inputRedisKeyElement ? this.inputRedisKeyElement?.nativeElement?.value : "";
		this.redisService
			.getAllKey(findKey)
			.pipe(
				takeUntil(this.ngUnsubscribe),
				finalize(() => this.indicator.hideActivityIndicator())
			)
			.subscribe((res) => {
				if (res?.content) {
					const page = this.pageIndex * this.pageSize;
					let dataTable = res.content?.map((item, index) => ({...item, stt: page + index + 1}));

					this.totalItem = dataTable.length;
					this.dataSource = new MatTableDataSource(dataTable);
					if (this.dataSource && !this.dataSource.paginator && this.dformPagination) {
						this.dataSource.paginator = this.dformPagination.paginator;
					}
				}
			});
	}

	onDeleteKey(key) {
		this.dialogService.confirm(
			{
				title: "Xác nhận",
				message: "Bạn chắc chắn muốn xóa key này?",
			},
			(result) => {
				if (result && key) {
					this.redisService
						.deleteKey(key)
						.pipe(takeUntil(this.ngUnsubscribe))
						.subscribe((res) => {
							this.getRedisList();
						});
				}
			}
		);
	}

	onDeleteAllKey() {
		this.dialogService.confirm(
			{
				title: "Xác nhận",
				message: "Bạn chắc chắn muốn xóa toàn bộ key?",
			},
			(result) => {
				if (result) {
					this.redisService
						.deleteAllKey()
						.pipe(takeUntil(this.ngUnsubscribe))
						.subscribe((res) => {
							this.getRedisList();
						});
				}
			}
		);
	}

	
}
