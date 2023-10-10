import { CollectionViewer, DataSource, SelectionChange } from "@angular/cdk/collections";
import {BehaviorSubject, merge, Observable} from 'rxjs';
import {FlatTreeControl} from '@angular/cdk/tree';
import { TreeNode } from "../models/category-resource.model";
import { CategoryResourceService } from "./category-resource.service";
import { map } from "rxjs/operators";

export class CategoryDataSource implements DataSource<TreeNode> {
    dataChange = new BehaviorSubject<TreeNode[]>([]);

    get data(): TreeNode[] {
        return this.dataChange.value;
    }
    set data(value: TreeNode[]) {
        this._treeControl.dataNodes = value;
        this.dataChange.next(value);
    }

    constructor(
        private _treeControl: FlatTreeControl<TreeNode>,
        private service: CategoryResourceService,
    ) { }

    connect(collectionViewer: CollectionViewer): Observable<TreeNode[]> {
        this._treeControl.expansionModel.changed.subscribe(change => {
            if (
                (change as SelectionChange<TreeNode>).added ||
                (change as SelectionChange<TreeNode>).removed
            ) {
                this.handleTreeControl(change as SelectionChange<TreeNode>);
            }
        });

        return merge(collectionViewer.viewChange, this.dataChange).pipe(map(() => this.data));
    }

    disconnect(collectionViewer: CollectionViewer): void { }

    /** Handle expand/collapse behaviors */
    handleTreeControl(change: SelectionChange<TreeNode>) {
        if (change.added) {
            change.added.forEach(node => this.toggleNode(node, true));
        }
        if (change.removed) {
            change.removed
                .slice()
                .reverse()
                .forEach(node => this.toggleNode(node, false));
        }
    }

    /**
     * Toggle the node, remove from display list
     */
    toggleNode(node: TreeNode, expand: boolean) {
        const children = this.service.getChildren(node.item);
        const index = this.data.indexOf(node);
        if (!children || index < 0) {
            // If no children, or cannot find the node, no op
            return;
        }

        node.isLoading = true;

        setTimeout(() => {
            if (expand) {
                const nodes = children.map(
                    element => {
                        return {
                            item: element,
                            level: node.level + 1,
                            expandable: element.hasChild,
                            isLoading : false,
                        };
                    },
                );
                this.data.splice(index + 1, 0, ...nodes);
            } else {
                let count = 0;
                for (
                    let i = index + 1;
                    i < this.data.length && this.data[i].level > node.level;
                    i++, count++
                ) { }
                this.data.splice(index + 1, count);
            }

            // notify the change
            this.dataChange.next(this.data);
            node.isLoading = false;
        }, 1000);
    }
}