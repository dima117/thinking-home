define(
	['app'],
	function(application) {

		application.module('Common', function (module, app, backbone, marionette, $, _) {
			
			module.SortableItemView = marionette.ItemView.extend({

				attributes: {
					"draggable": true
				},

				dragEvents: {
					"dragstart": "start",
					"dragenter": "enter",
					"dragleave": "leave",
					"dragend": "leave",
					"dragover": "over",
					"drop": "drop"
				},

				initialize: function (options) {
					_.extend(this, options);
				},

				onRender: function () {
					this.$el.addClass('sortable-view-item');
				},

				// Adds the drag events
				delegateEvents: function (events) {
					var ev = _.extend({}, events, this.dragEvents);
					marionette.View.prototype.delegateEvents.call(this, ev);
				},

				start: function (e) {
					this.parent.draggedModel = this.model;

					if (e.originalEvent) {
						e = e.originalEvent;
					}

					e.dataTransfer.effectAllowed = "move";
					e.dataTransfer.dropEffect = "move";
					e.dataTransfer.setData('text', "Drag");
				},

				enter: function (e) {
					e.preventDefault();
					this.$el.addClass(this.overClass);
				},

				leave: function (e) {
					e.preventDefault();
					this.$el.removeClass(this.overClass);
				},

				over: function (e) {
					e.preventDefault();
					return false;
				},

				drop: function (e) {
					e.preventDefault();
					this.leave(e);
					var collection = this.model.collection,
					parent = this.parent,
					currentIndex = this.$el.index();

					collection.remove(parent.draggedModel);
					collection.add(parent.draggedModel, { at: currentIndex });

					this.trigger('drop', this.parent.draggedModel);
				}

			});

			module.SortableCollectionView = marionette.CompositeView.extend({
			
				onRender: function () {
					
					this.$childViewContainer.addClass('sortable-view');
				},

				childView: module.SortableItemView,

				overClass: 'over',

				childEvents: {
					'childview:drop': 'onDropItem'
				},

				delegateEvents: function (events) {
					marionette.View.prototype.delegateEvents.call(this, events);
					marionette.bindEntityEvents(this, this, marionette.getOption(this, 'childEvents'));
				},

				buildChildView: function (item, childViewType, childViewOptions) {
					var options = _.extend({
						model: item,
						overClass: this.overClass,
						parent: this
					}, childViewOptions);

					return new childViewType(options);
				},

				appendHtml: function (collectionView, childView, index) {
					var childrenContainer = collectionView.childViewContainer
						? collectionView.$(collectionView.childViewContainer)
						: collectionView.$el;

					var children = childrenContainer.children();

					if (children.size() <= index) {
						childrenContainer.append(childView.el);
					} else {
						childrenContainer.children().eq(index).before(childView.el);
					}
				},

				onDropItem: function (model) {
					//console.log('DROPPED ITEM', model);
				}

			});
		});
		
		return application.Common;
	});

