﻿import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    SessionServiceProxy,
    UserLoginInfoDto,
    TenantLoginInfoDto,
    ApplicationInfoDto,
    PaymentServiceProxy
} from '@shared/service-proxies/service-proxies';
import { SubscriptionStartType, EditionPaymentType } from "@shared/AppEnums";
import { AppSessionService } from '@shared/common/session/app-session.service';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { ActivatedRoute } from '@angular/router';
import { DataTable } from 'primeng/components/datatable/datatable';
import { Paginator } from 'primeng/components/paginator/paginator';

@Component({
    templateUrl: './subscription-management.component.html',
    animations: [appModuleAnimation()]
})

export class SubscriptionManagementComponent extends AppComponentBase implements OnInit {

    @ViewChild('dataTable') dataTable: DataTable;
    @ViewChild('paginator') paginator: Paginator;

    loading: boolean;
    user: UserLoginInfoDto = new UserLoginInfoDto();
    tenant: TenantLoginInfoDto = new TenantLoginInfoDto();
    application: ApplicationInfoDto = new ApplicationInfoDto();
    subscriptionStartType: typeof SubscriptionStartType = SubscriptionStartType;
    editionPaymentType: EditionPaymentType = EditionPaymentType;

    filterText: string = '';

    constructor(
        injector: Injector,
        private _sessionService: SessionServiceProxy,
        private _paymentServiceProxy: PaymentServiceProxy,
        private _appSessionService: AppSessionService,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngOnInit(): void {
        this.getSettings();
    }

    getSettings(): void {
        this.loading = true;
        this._appSessionService.init().then(() => {
            this.loading = false;
            this.user = this._appSessionService.user;
            this.tenant = this._appSessionService.tenant;
            this.application = this._appSessionService.application;
        });
    }

    getPaymentHistory(event?: LazyLoadEvent) {
        this.primengDatatableHelper.showLoadingIndicator();

        this._paymentServiceProxy.getPaymentHistory(
            this.primengDatatableHelper.getSorting(this.dataTable),
            this.primengDatatableHelper.getMaxResultCount(this.paginator, event),
            this.primengDatatableHelper.getSkipCount(this.paginator, event)
        ).subscribe(result => {
            this.primengDatatableHelper.totalRecordsCount = result.totalCount;
            this.primengDatatableHelper.records = result.items;
            this.primengDatatableHelper.hideLoadingIndicator();
        });
    }
}