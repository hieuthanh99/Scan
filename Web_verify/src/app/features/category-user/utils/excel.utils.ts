import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root',
})
export class ExcelUtils {
	readExcelFile(event?: any, SHEET_NAME?: string, HEADER = 1): Promise<any> {
		return new Promise(resolve => {
			// const target: DataTransfer = (event.target) as DataTransfer;
			// if (target.files.length !== 1) { throw new Error('Cannot use multiple files'); }
			// const reader: FileReader = new FileReader();
			// reader.onload = (e: any) => {
			// 	/* read workbook */
			// 	const bstr: string = e.target.result;
			// 	const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });
			// 	// let wsname: string = wb.SheetNames[0];
			// 	if (SHEET_NAME && wb.SheetNames.includes(SHEET_NAME)) {
			// 		// wsname = SHEET_NAME;
			// 		const ws: XLSX.WorkSheet = wb.Sheets[SHEET_NAME];
			// 		/* save data */
			// 		const data = XLSX.utils.sheet_to_json(ws, { header: HEADER, raw: false });
			// 		resolve(data);
			// 	}
			// 	else {
			// 		resolve(false);
			// 	}
			// };
			// reader.readAsBinaryString(target.files[0]);
			resolve(true);
		});
	}
}