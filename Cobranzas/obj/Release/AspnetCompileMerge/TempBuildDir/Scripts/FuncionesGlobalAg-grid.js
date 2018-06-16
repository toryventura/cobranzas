function setDataSource(allOfTheData, grid) {
    //var dataSource = {
    //    //rowCount: ???, - not setting the row count, infinite paging will be used
    //    pageSize: 500,
    //    overflowSize: 500,
    //    getRows: function (params) {
    //        // this code should contact the server for rows. however for the purposes of the demo,
    //        // the data is generated locally, and a timer is used to give the expereince of
    //        // an asynchronous call
    //        console.log('asking for ' + params.startRow + ' to ' + params.endRow);
    //        setTimeout(function () {
    //            // take a chunk of the array, matching the start and finish times
    //            var rowsThisPage = allOfTheData.slice(params.startRow, params.endRow);
    //            var lastRow = -1;
    //            // see if we have come to the last page, and if so, return it
    //            if (allOfTheData.length <= params.endRow) {
    //                lastRow = allOfTheData.length;
    //            }
    //            params.successCallback(rowsThisPage, lastRow);
    //        }, 500);
    //    }
    //};
    grid.api.setRowData(allOfTheData);
    //grid.api.setDatasource(dataSource);

}

function createGlobal(gridOptions, gridDiv) {
    new agGrid.Grid(gridDiv, gridOptions);
}



function destroyGlobal(grid) {
    grid.api.destroy();
}
