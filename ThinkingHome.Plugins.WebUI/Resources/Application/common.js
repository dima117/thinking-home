define([
	'app',
	'json!api/webui/styles.json',
	'application/common/complex-view',
	'application/common/form-view',
	'application/common/sortable-view',
	'application/common/utils'],
	function (application, cssFiles, complexView, formView, sortableView, utils) {

		var common = {
			ComplexView: complexView,
			FormView: formView,
			SortableItemView: sortableView.SortableItemView,
			SortableCollectionView: sortableView.SortableCollectionView,
			utils: utils
		};

		utils.loadCss.apply(null, cssFiles);
		utils.displayCurrentTime('.js-cur-time');

		return common;
	});