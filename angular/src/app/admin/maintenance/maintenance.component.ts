﻿import { Component, Injector, OnInit, AfterViewInit } from '@angular/core';
import { CachingServiceProxy, EntityDtoOfString, WebLogServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from "lodash";
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
    templateUrl: "./maintenance.component.html",
    styleUrls: ["./maintenance.component.less"],
    animations: [appModuleAnimation()]
})
export class MaintenanceComponent extends AppComponentBase implements OnInit, AfterViewInit {

    constructor(
        injector: Injector,
        private _cacheService: CachingServiceProxy,
        private _webLogService: WebLogServiceProxy,
        private _fileDownloadService: FileDownloadService) {
        super(injector);
    }

    loading: boolean = false;
    caches: any = null;
    logs: any = '';

    getCaches(): void {
        let self = this;
        self.loading = true;
        self._cacheService.getAllCaches()
            .finally(() => {
                self.loading = false;
            })
            .subscribe((result) => {
                self.caches = result.items;
            });
    };

    clearCache(cacheName): void {
        let self = this;
        let input = new EntityDtoOfString();
        input.id = cacheName;
        
        self._cacheService.clearCache(input).subscribe(() => {
            self.notify.success(self.l('CacheSuccessfullyCleared'));
        });
    }

    clearAllCaches(): void {
        let self = this;
        self._cacheService.clearAllCaches().subscribe(() => {
            self.notify.success(self.l('AllCachesSuccessfullyCleared'));
        });
    };

    getWebLogs(): void {
        let self = this;
        self._webLogService.getLatestWebLogs().subscribe((result) => {
            self.logs = result.latestWebLogLines;
            self.fixWebLogsPanelHeight();
        });
    };

    downloadWebLogs = function () {
        let self = this;
        self._webLogService.downloadWebLogs().subscribe((result) => {
            self._fileDownloadService.downloadTempFile(result);
        });
    };

    getLogClass(log: string): string {

        if (log.startsWith("DEBUG")) {
            return "label label-default";
        }

        if (log.startsWith("INFO")) {
            return "label label-info";
        }

        if (log.startsWith("WARN")) {
            return "label label-warning";
        }

        if (log.startsWith("ERROR")) {
            return "label label-danger";
        }

        if (log.startsWith("FATAL")) {
            return "label label-danger";
        }

        return "";
    }

    getLogType(log: string): string {
        if (log.startsWith("DEBUG")) {
            return "DEBUG";
        }

        if (log.startsWith("INFO")) {
            return "INFO";
        }

        if (log.startsWith("WARN")) {
            return "WARN";
        }

        if (log.startsWith("ERROR")) {
            return "ERROR";
        }

        if (log.startsWith("FATAL")) {
            return "FATAL";
        }

        return "";
    }

    getRawLogContent(log: string): string {
        return _.escape(log)
            .replace('DEBUG', '')
            .replace('INFO', '')
            .replace('WARN', '')
            .replace('ERROR', '')
            .replace('FATAL', '');
    }

    fixWebLogsPanelHeight(): void {
        var windowHeight = $(window).height();
        var panelHeight = $('.full-height').height();
        var difference = windowHeight - panelHeight;
        var fixedHeight = panelHeight + difference;
        $('.full-height').css('height', (fixedHeight - 350) + 'px');
    }

    ngAfterViewInit(): void {
        $(window).bind('resize', () => {
            this.fixWebLogsPanelHeight();
        });
    }

    ngOnInit(): void {
        let self = this;
        self.getCaches();
        self.getWebLogs();
    }
}