﻿using Abp.Application.Services.Dto;
using System;

namespace Pb.Wechat.MpMessages.Dto
{
    public class MpMessageMultiArticlesDataListOutput : EntityDto<int>
    {
        public DateTime? LastModificationTime { get; set; }
        public string MessageType { get; set; }
        public int? ArticleGroupID { get; set; }
        public int? ArticleID { get; set; }
        public string FilePathOrUrl { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int RowSpan { get; set; }
        public string State { get; set; }
        public DateTime? ExecTaskTime { get; set; }
        public int IsTask { get; set; }
        public long? SendCount { get; set; }
        public DateTime? FinishDate { get; set; }
        public long? FailCount { get; set; }
        public long? SuccessCount { get; set; }
        public int SendState { get; set; }
    }
}
