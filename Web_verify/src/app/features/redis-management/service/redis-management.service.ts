import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "@env/environment";
import { HttpClientService, HttpOptions } from "@shared-sm";
import { findIndex, map } from "rxjs";
import { PATH } from "../constants";
import Utils from "src/app/shared/utils/utils";

@Injectable({
	providedIn: "root",
})
export class RedisManagementService {
	constructor(
		private httpClient: HttpClientService,
		private http: HttpClient
	) {}

	public getAllKey(findkey : String) {
		const options: HttpOptions = {
			path: PATH.LIST,
			url: environment.hostApi,
			params: {
				prefix: findkey ? `${findkey}*` : "*"
			},
		};

		return this.httpClient.get(options).pipe(
			map((response: any) => {
				return response;
			})
		);
	}

	public deleteAllKey() {
		const options: HttpOptions = {
			path: PATH.DELETE_ALL_KEY,
			url: environment.hostApi,
			params: {},
		};

		return this.httpClient.post(options).pipe(
			map((response: any) => {
				return response;
			})
		);
	}

	public deleteKey(key: String) {
		const options: HttpOptions = {
			path: PATH.DELETE_KEY,
			url: environment.hostApi,
			params: {
				key: key,
			},
		};

		return this.httpClient.post(options).pipe(
			map((response: any) => {
				return response;
			})
		);
	}
}
