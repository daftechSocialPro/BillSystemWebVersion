import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
//import { SelectList } from 'src/models/ResponseMessage.Model';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { IBillSectionDto } from 'src/models/system-control/IBillSectionDto';
import { IGeneralSettingDto } from 'src/models/system-control/IGeneralSettingDto';
import { IKebelesDto } from 'src/models/system-control/IKebelesDto';
import { IKetenaDto } from 'src/models/system-control/IKetenaDto';

interface SelectList {
  id: string;
  name: string;
}
interface ExtractedProperties {
  sdPaid: string;
}
@Component({
  selector: 'app-css-batch-records',
  templateUrl: './css-batch-records.component.html',
  styleUrls: ['./css-batch-records.component.scss']
})
export class CssBatchRecordsComponent implements OnInit {
  @Input() Customer: ICustomerDto[];
  paginationCustomer: ICustomerDto[] = [];
  totlRecords: number = 0;
  searchText: string = '';
  first: number = 0;
  rows: number = 5;
  batchValues: SelectList[] = [];
  MeterClasses: IGeneralSettingDto[];
  BillCycle: IGeneralSettingDto[];
  BillOffice: IBillSectionDto[];
  SdPaid: ICustomerDto[];
  filterdInterface: ICustomerDto[] = [];
  selectedProperty = [];
  ketena: IKetenaDto[];
  kebele: IKebelesDto[];
  selectedBatchValue: string = '';
  selectedBatchValue2: string = '';
  selectedKetena: string = '';
  selectedRows: { [key: string]: boolean } = {};
  filteredCustomer = [];
  changeFiledNames = ['BankCode', 'billSalesGroup', 'meterClass', 'meterDigit', 'BillCycle', 'Reader Name', 'SweragePaid'];
  searchBy = ['Ketena', 'Kebele', 'Customer Category'];

  // property:any[]=[]

  paginationCustomerValues: {
    bankAccount: string;
    bookNo: string;
    meterClass: string;
    readerName: string;
    meterDigit: number;
    paymentMod: string;
    village: string;
    watersource: string;
  }[] = [];

  constructor(
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private activeModal: NgbActiveModal,
    private scsMaintainService: ScsMaintainService,
    private customerService: CssCustomerService,
    private controlService: ScsDataService
  ) {}

  ngOnInit(): void {
    this.getCustomerForBatchRecord();
  }

  selectAll(checked: boolean) {
    for (const item of this.paginationCustomer) {
      this.selectedRows[item.custId] = checked;
    }
  }

  handleCheckboxChange(event: any) {
    const isChecked = event.target.checked;
    this.selectAll(isChecked);
  }
  isRowSelected(itemId: string): boolean {
    return this.selectedRows[itemId] || false;
  }

  // Function to check if all rows are selected
  areAllRowsSelected(): boolean {
    return this.paginationCustomer.length > 0 && this.paginationCustomer.every((item) => this.selectedRows[item.custId]);
  }

  filterInterfaces() {
    if (this.searchText.trim() === '') {
      this.filterdInterface = this.Customer;
    } else {
      this.filterdInterface = this.Customer.filter(
        (item) =>
          item.customerName.toLowerCase().includes(this.searchText.toLowerCase()) ||
          item.contractNo.toLowerCase().includes(this.searchText.toLowerCase())
      );
    }
    this.first = 0;
    this.onPageChange({ first: this.first, rows: this.rows }, this.filterdInterface);
  }

  saveSelectedBatchValues() {
    const selectedCustomers = this.paginationCustomer.filter(customer => this.selectedRows[customer.custId]);

    this.customerService.updateCustomer(this.SdPaid).subscribe({
      next: (res) => {
        if (res.success) {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Batch values saved successfully for selected customers'
          });
        } else {
          this.messageService.add({ severity: 'error', summary: 'Something went wrong!!!', detail: res.message });
        }
      },
      error: (error) => {
        // Handle error
        console.error('Error saving batch values for selected customers:', error);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to save batch values for selected customers' });
      }
    });
  }

  //   saveBatchValues() {
  //     const selectedBatchValues = this.Customer.filter(value => {
  //       return this.selectedBatchValue === value.custId || this.selectedBatchValue2 === value.custId;
  //     });
  //     console.log('Selected Batch Values:', selectedBatchValues);

  //     this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Batch values saved successfully' });
  // }

  getCustomerForBatchRecord() {
    this.customerService.getCustomer().subscribe({
      next: (res) => {
        this.Customer = res;
        this.paginationCustomerValues = this.Customer.map((item: ICustomerDto) => ({
          bankAccount: item.bankAccount,
          bookNo: item.bookNo,
          meterClass: item.meterClass,
          readerName: item.readerName,
          meterDigit: item.meterDigit,
          paymentMod: item.paymentMode,
          village: item.village,
          watersource: item.waterSource
        }));
        this.selectedProperty = this.paginationCustomerValues;
        this.paginatedCustomer(res);
        console.log(this.paginationCustomerValues);
      },
      error: (error) => {
        console.error('Error fetching customer data:', error);
      }
    });
  }

  getBanks() {
    this.scsMaintainService.getBanks().subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.empId,
            name: item.name
          };
        });
      }
    });
  }
  // distinctRentGroupCodes = [...new Set(res.map(record => record.rentGroupCode))];
  //       this.paginatedMeterRate(this.MeterRates)

  getKetenas() {
    this.controlService.getKetena().subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.ketenaCode.toString(),
            name: item.ketenaName
          };
        });
      }
    });
  }
  getKebeles() {
    this.controlService.getKebeles().subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.kebeleCode.toString(),
            name: item.kebeleName
          };
        });
      }
    });
  }
  getCustomerCategory() {
    this.controlService.getCustomerCategory().subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.custCategoryCode.toString(),
            name: item.custCategoryName
          };
        });
      }
    });
  }
  getBillOficers() {
    this.scsMaintainService.getBillOffciers().subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.id,
            name: item.name
          };
        });
      }
    });
  }

  getMeterClasses() {
    this.controlService.getGeneralSetting('meterClass').subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.recordno,
            name: item.inputValues
          };
        });
        console.log(res);
      }
    });
  }
  getBillCycle() {
    this.controlService.getGeneralSetting('BOOK NUMBER').subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.recordno,
            name: item.inputValues
          };
        });
        console.log(res);
      }
    });
  }
  getBillOfficers() {
    this.scsMaintainService.getBillOffciersForTransfer().subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.id,
            name: item.name
          };
        });
        console.log(res);
      }
    });
  }

  SearchCustomer(group: string, valueSearch: string) {
    console.log(group, valueSearch, this.Customer);
    if (group.toLowerCase() == 'ketena') {
      this.paginationCustomer = this.Customer.filter((customer) => customer.ketena.toLowerCase() === valueSearch.toLowerCase());
      this.totlRecords = this.paginationCustomer.length;
    } else if (group.toLowerCase() == 'kebele') {
      this.paginationCustomer = this.Customer.filter((customer) => Number(customer.kebele) === Number(valueSearch));
      this.totlRecords = this.paginationCustomer.length;
      //this.paginatedCustomer(this.Customer);
    } else if (group.toLowerCase() == 'Customer Category') {
      this.paginationCustomer = this.Customer.filter((customer) => Number(customer.custCategoryCode) == Number(valueSearch));
      this.totlRecords = this.paginationCustomer.length;
    } else {
      this.paginatedCustomer(this.Customer);
    }
  }

  searcFilter(value: string) {
    if (value === 'Ketena') {
      this.getKetenas();
    }

    if (value == 'Kebele') {
      this.getKebeles();
    } else if (value == '') {
      this.batchValues = [];
    }
    if (value === 'Customer Category') {
      this.getCustomerCategory();
    }
  }
  filter(value: string) {
    this.selectedBatchValue = value;
    if (value == 'BankCode') {
      this.getBanks();
    }

    if (value == 'billSalesGroup') {
      this.getBillOficers();
    }

    if (value == 'BillCycle') {
      this.getBillCycle();
    } else if (value == '') {
      this.batchValues = [];
    }
    if (value == 'Reader Name') {
      this.getBillOfficers();
    } else if (value == '') {
      this.batchValues = [];
    }

    if (value == 'SweragePaid') {
      this.batchValues = [
        { id: 'YES', name: 'YES' },
        { id: 'NO', name: 'NO' }
      ];
    }
  }

  onPageChange(event: any, gInterface?: ICustomerDto[]) {
    this.first = event.first;
    this.rows = event.rows;
    if (gInterface) {
      this.paginatedCustomer(gInterface);
    } else {
      this.paginatedCustomer(this.Customer);
    }
  }
  paginatedCustomer(ginterfces: ICustomerDto[]) {
    this.totlRecords = ginterfces.length;
    this.paginationCustomer = ginterfces.slice(this.first, this.first + this.rows);
  }
  closeModal() {
    this.activeModal.close();
  }
}
